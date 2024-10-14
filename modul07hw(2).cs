using System;
using System.Collections.Generic;

// Интерфейс для наблюдателей (подписчиков)
public interface IObserver
{
    void Update(string currency, decimal rate);
}

// Интерфейс для субъекта (наблюдаемого объекта)
public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}

// Реализация класса субъекта (обновление курсов валют)
public class CurrencyExchange : ISubject
{
    private Dictionary<string, decimal> _currencyRates = new Dictionary<string, decimal>();
    private List<IObserver> _observers = new List<IObserver>();

    // Регистрация наблюдателя
    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
        Console.WriteLine($"{observer.GetType().Name} подписан на уведомления.");
    }

    // Удаление наблюдателя
    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
        Console.WriteLine($"{observer.GetType().Name} отписан от уведомлений.");
    }

    // Уведомление всех наблюдателей об изменении курса валют
    public void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            foreach (var currency in _currencyRates)
            {
                observer.Update(currency.Key, currency.Value);
            }
        }
    }

    // Метод для изменения курса валюты
    public void SetCurrencyRate(string currency, decimal rate)
    {
        _currencyRates[currency] = rate;
        Console.WriteLine($"Курс {currency} изменен на {rate:C}");
        NotifyObservers();
    }
}

// Наблюдатель для трейдеров
public class Trader : IObserver
{
    private string _name;

    public Trader(string name)
    {
        _name = name;
    }

    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"{_name} получил обновление: Курс {currency} теперь {rate:C}");
    }
}

// Наблюдатель для банков
public class Bank : IObserver
{
    private string _bankName;

    public Bank(string bankName)
    {
        _bankName = bankName;
    }

    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"{_bankName} получил обновление: Курс {currency} составляет {rate:C}");
    }
}

// Наблюдатель для мобильного приложения
public class MobileApp : IObserver
{
    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"Мобильное приложение обновлено: Курс {currency} изменился на {rate:C}");
    }
}

// Клиентский код
class Program
{
    static void Main(string[] args)
    {
        // Создаем субъект (Биржу валют)
        CurrencyExchange exchange = new CurrencyExchange();

        // Создаем нескольких наблюдателей
        Trader trader1 = new Trader("Трейдер Иван");
        Bank bank1 = new Bank("Банк А");
        MobileApp app = new MobileApp();

        // Подписываем наблюдателей на уведомления
        exchange.RegisterObserver(trader1);
        exchange.RegisterObserver(bank1);
        exchange.RegisterObserver(app);

        // Изменяем курс валюты и уведомляем наблюдателей
        exchange.SetCurrencyRate("USD", 420.75m);
        exchange.SetCurrencyRate("EUR", 495.50m);

        // Отписываем одного из наблюдателей
        exchange.RemoveObserver(trader1);

        // Снова изменяем курс валюты
        exchange.SetCurrencyRate("USD", 430.00m);
    }
}