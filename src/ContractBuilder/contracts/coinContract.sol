pragma solidity ^0.4.9;
import "./coin.sol";
import "./token/erc20Contract.sol";

contract ColorCoin is Coin(0){

    address _externalTokenAddress;

    function ColorCoin(address exchangeContractAddress, address externalTokenAddress) Coin(exchangeContractAddress) { 
        _externalTokenAddress = externalTokenAddress;
    }

    function cashin(address receiver, uint amount) onlyowner payable {
        if (msg.value > 0) throw; 
        
        var userAddress = transferContractUser[receiver];

        if (userAddress == address(0)) {
            throw;
        }

        coinBalanceMultisig[userAddress] += amount;

        CoinCashIn(userAddress, amount);
    }

    // cashout coins (called only from exchange contract)
    function cashout(address from, address to, uint amount, bytes32 hash, bytes client_sig, bytes params) onlyFromExchangeContract { 
        if (!_checkClientSign(from, hash, client_sig)) {
            throw;                    
        }

        if (coinBalanceMultisig[from] < amount) {
            throw;
        }

        var erc20Token = ERC20Interface(_externalTokenAddress);
        var tokenBalance = erc20Token.balanceOf(this);

        if (tokenBalance < amount) {
            throw;
        }

        if (!erc20Token.transfer(to, amount)){
            throw;
        }

        coinBalanceMultisig[from] -= amount;

        CoinCashOut(msg.sender, from, amount, to);
    }
}