using System;

public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

public class CreditCardPaymentStrategy : IPaymentStrategy
{
    private string _cardNumber;
    private string _cardHolder;
    
    public CreditCardPaymentStrategy(string cardNumber, string cardHolder)
    {
        _cardNumber = cardNumber;
        _cardHolder = cardHolder;
    }

    public void Pay(decimal amount)
    {
        Console.WriteLine($"Оплата банковской картой {_cardHolder}: {amount:C} успешно проведена.");
    }
}

public class PayPalPaymentStrategy : IPaymentStrategy
{
    private string _email;
    
    public PayPalPaymentStrategy(string email)
    {
        _email = email;
    }

    public void Pay(decimal amount)
    {
        Console.WriteLine($"Оплата через PayPal с аккаунта {_email}: {amount:C} успешно проведена.");
    }
}

public class CryptoPaymentStrategy : IPaymentStrategy
{
    private string _walletAddress;
    
    public CryptoPaymentStrategy(string walletAddress)
    {
        _walletAddress = walletAddress;
    }

    public void Pay(decimal amount)
    {
        Console.WriteLine($"Оплата криптовалютой с кошелька {_walletAddress}: {amount:C} успешно проведена.");
    }
}

public class PaymentContext
{
    private IPaymentStrategy _paymentStrategy;

    public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void ExecutePayment(decimal amount)
    {
        if (_paymentStrategy == null)
        {
            throw new InvalidOperationException("Стратегия оплаты не установлена.");
        }
        _paymentStrategy.Pay(amount);
    }
}

class Program
{
    static void Main(string[] args)
    {
        PaymentContext paymentContext = new PaymentContext();
        
        paymentContext.SetPaymentStrategy(new CreditCardPaymentStrategy("1234-5678-9876-5432", "Иван Иванов"));
        paymentContext.ExecutePayment(100.50m);

        paymentContext.SetPaymentStrategy(new PayPalPaymentStrategy("ivan@example.com"));
        paymentContext.ExecutePayment(200.75m);

        paymentContext.SetPaymentStrategy(new CryptoPaymentStrategy("0xAbc123Def456"));
        paymentContext.ExecutePayment(0.045m);
    }
}