
using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.FactoryMethod.Conceptual
{
    // Если объект принять за колонку с разными вариациями то фабрчный метод 
    // это создание конкретной ячейки в этой колонке
    class Program
    {
        static void Main(string[] args)
        {
            
            var inputTextSimple = new ClientInputText().MainConcreteInputTextSimple();

            var inputTextMultySelect = new ClientInputText().MainConcreteInputTextMultySelect();
        }
    }


    class ClientInputText
    {
        public InputTextUI MainConcreteInputTextSimple()
        {
            return ClientCode(new ConcreteInputTextSimple());
        }

        public InputTextUI MainConcreteInputTextMultySelect()
        {
            return ClientCode(new ConcreteInputTextMultySelect());
        }

        // The client code works with an instance of a concrete creator, albeit
        // through its base interface. As long as the client keeps working with
        // the creator via the base interface, you can pass it any creator's
        // subclass.
        public InputTextUI ClientCode(CreatorInputText creator)
        {
            return new InputTextUI
            {
                Name = creator.FactoryInputText().CreateInputText().Name,
                MultySelectOptions = creator.FactoryInputText().CreateInputText().MultySelectOptions
            };
        }
    }

    // The Creator class declares the factory method that is supposed to return
    // an object of a Product class. The Creator's subclasses usually provide
    // the implementation of this method.
    abstract class CreatorInputText
    {
        // Note that the Creator may also provide some default implementation of
        // the factory method.
        public abstract IInputText FactoryInputText();

        // Also note that, despite its name, the Creator's primary
        // responsibility is not creating products. Usually, it contains some
        // core business logic that relies on Product objects, returned by the
        // factory method. Subclasses can indirectly change that business logic
        // by overriding the factory method and returning a different type of
        // product from it.
        public string SomeOperation()
        {
            // Call the factory method to create a Product object.
            var product = FactoryInputText();
            // Now, use the product.
            var result = "Creator: The same creator's code has just worked with "
                + product.CreateInputText();

            return result;
        }
    }

    // Concrete Creators override the factory method in order to change the
    // resulting product's type.
    class ConcreteInputTextSimple : CreatorInputText
    {
        // Note that the signature of the method still uses the abstract product
        // type, even though the concrete product is actually returned from the
        // method. This way the Creator can stay independent of concrete product
        // classes.
        public override IInputText FactoryInputText()
        {
            return new InputTextSimple();
        }
    }

    class ConcreteInputTextMultySelect : CreatorInputText
    {
        public override IInputText FactoryInputText()
        {
            return new InputTextMultySelect();
        }
    }

    // The Product interface declares the operations that all concrete products
    // must implement.
    public interface IInputText
    {
        InputText CreateInputText();
    }

    // Concrete Products provide various implementations of the Product
    // interface.
    class InputTextSimple : IInputText
    {
        public InputText CreateInputText()
        {
            return new InputText {
                Name = "InputTextSimple"
            };
        }
    }

    class InputTextMultySelect : IInputText
    {
        public InputText CreateInputText()
        {
            return new InputText
            {
                Name = "InputTextMultySelect",
                MultySelectOptions = new List<string>
                {
                    "NameFirst", "NameSecond"
                }
            };
        }
    }




    public class InputText
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public List<string> MultySelectOptions { get; set; }
    }

    public class InputTextUI
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public List<string> MultySelectOptions { get; set; }
    }
}






