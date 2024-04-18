using System;

public interface IDocument
{
    void DisplayContent();
    void SetContent(string newContent);
    void ClearContent();
}
class Document: IDocument
{
    public string content;

    public Document()
    {
        content = "";
    }

    public Document(string content)
    {
        this.content = content;
    }

    public Document(Document other)
    {
        content = other.content;
    }

    public void DisplayContent()
    {
        Console.WriteLine("Содержимое документа: " + content);
    }

    public void SetContent(string newContent)
    {
        content = newContent;
    }

    public void ClearContent()
    {
        content = "";
    }

    public Document Copy()
    {
        return new Document(content);
    }
}

class EncryptedDocument : Document
{
    private string key;

    public EncryptedDocument() : base()
    {
        key = "";
    }

    public EncryptedDocument(string content, string key) : base(content)
    {
        this.key = key;
    }

    public EncryptedDocument(EncryptedDocument other) : base(other)
    {
        key = other.key;
    }

    public void Encrypt()
    {
        content += "!";
        Console.WriteLine("Содержимое зашифровано");
    }

    public void Decrypt()
    {

        if (content.EndsWith("!"))
        {
            content = content.TrimEnd('!');
            Console.WriteLine("Содержимое дешифровано.");
        }
        else
        {
            Console.WriteLine("Контент не дешифрован");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Document document = null;

        while (true)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Создать документ");
            Console.WriteLine("2. Копировать документ");
            Console.WriteLine("3. Зашифровать документ");
            Console.WriteLine("4. Дешифровать документ");
            Console.WriteLine("5. Показать содержимое документа");
            Console.WriteLine("6. Очистить документ");
            Console.WriteLine("7. Выйти из программы");

            Console.Write("Что вы хотите сделать?: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите содержимое документа: ");
                    string content = Console.ReadLine();
                    document = new Document(content);
                    Console.WriteLine("Документ создан");
                    break;
                case "2":
                    if (document != null)
                    {
                        Document copiedDocument = document.Copy();
                        Console.WriteLine("Содержимое документа скопировано");
                    }
                    else
                    {
                        Console.WriteLine("Документ еще не был создан");
                    }
                    break;
                case "3":
                    if (document != null)
                    {
                        Console.Write("Введите ключ шифрования - ");
                        string key = Console.ReadLine();
                        document = new EncryptedDocument(document.content, key);
                        ((EncryptedDocument)document).Encrypt();
                    }
                    else
                    {
                        Console.WriteLine("Документ еще не был создан");
                    }
                    break;
                case "4":
                    if (document != null && document is EncryptedDocument)
                    {
                        ((EncryptedDocument)document).Decrypt();
                    }
                    else
                    {
                        Console.WriteLine("Документ еще не был создан или не был зашифрован");
                    }
                    break;
                case "5":
                    if (document != null)
                    {
                        document.DisplayContent();
                    }
                    else
                    {
                        Console.WriteLine("Документ еще не был создан");
                    }
                    break;
                case "6":
                    if (document != null)
                    {
                        document.ClearContent();
                        Console.WriteLine("Содержимое документа очищено");
                    }
                    else
                    {
                        Console.WriteLine("Документ еще не был создан");
                    }
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Неизвестная команда");
                    break;
            }
        }
    }
}