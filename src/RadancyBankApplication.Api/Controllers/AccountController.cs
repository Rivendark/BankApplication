using Microsoft.AspNetCore.Mvc;
using RadancyBankApplication.Api.DTOs;
using RadancyBankApplication.Application.Repositories;
using RadancyBankApplication.Application.Services;
using RadancyBankApplication.Core.Enums;
using RadancyBankApplication.Core.Exceptions;

namespace RadancyBankApplication.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountValidationService _validationService;

    public AccountController(IAccountRepository accountRepository, IAccountValidationService validationService)
    {
        _accountRepository = accountRepository;
        _validationService = validationService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AccountDto account, CancellationToken token)
    {
        try
        {
            await _accountRepository.CreateAccountAsync(account.ToDomainModel(), token);
        }
        catch (AccountExistsException aeEx)
        {
            return BadRequest(aeEx.Message);
        }

        return Accepted(account);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] AccountDto account, CancellationToken token)
    {
        try
        {
            await _accountRepository.UpdateAccountInformationAsync(account.ToDomainModel(), token);
        }
        catch (AccountNotFoundException anfEx)
        {
            return BadRequest(anfEx.Message);
        }

        return Accepted(account);
    }

    [HttpDelete("delete/{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
    {
        try
        {
            await _accountRepository.DeleteAccountAsync(id, token);
        }
        catch (AccountNotFoundException anfEx)
        {
            return BadRequest(anfEx.Message);
        }

        return Accepted(id);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetAccount([FromRoute] Guid id, CancellationToken token)
    {
        var result = await _accountRepository.GetAccountAsync(id, token);

        if (result is null)
        {
            return NotFound(id);
        }

        return Ok(result);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] BalanceChangeDto balanceChange, CancellationToken token)
    {
        var account = await _accountRepository.GetAccountAsync(balanceChange.AccountId, token);
        if (account is null)
        {
            return BadRequest("Account Not Found");
        }

        try
        {
            var balanceChangeModel = balanceChange.ToDomainModel(BalanceChangeType.Withdrawal);
            _validationService.ValidateWithdrawal(account, balanceChangeModel);

            await _accountRepository.UpdateAccountBalanceAsync(
                balanceChangeModel.AccountId,
                balanceChangeModel.Amount,
                balanceChangeModel.Type,
                token);
            await _accountRepository.SaveBalanceChangeAsync(balanceChangeModel, token);
            account = await _accountRepository.GetAccountAsync(balanceChange.AccountId, token);
        }
        catch (InsufficientAccountBalanceException afnEx)
        {
            return BadRequest(afnEx.Message);
        }
        catch (WithdrawalPercentageExceededException wpeEx)
        {
            return BadRequest(wpeEx.Message);
        }

        return Accepted(account);
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] BalanceChangeDto balanceChange, CancellationToken token)
    {
        var account = await _accountRepository.GetAccountAsync(balanceChange.AccountId, token);
        if (account is null)
        {
            return BadRequest("Account Not Found");
        }
        
        try
        {
            var balanceChangeModel = balanceChange.ToDomainModel(BalanceChangeType.Deposit);
            _validationService.ValidateDeposit(account, balanceChangeModel);

            await _accountRepository.UpdateAccountBalanceAsync(
                balanceChangeModel.AccountId,
                balanceChangeModel.Amount,
                balanceChangeModel.Type,
                token);
            await _accountRepository.SaveBalanceChangeAsync(balanceChangeModel, token);
            account = await _accountRepository.GetAccountAsync(balanceChange.AccountId, token);
        }
        catch (DepositLimitExceededException dleEx)
        {
            return BadRequest(dleEx.Message);
        }
        
        return Accepted(account);
    }
}