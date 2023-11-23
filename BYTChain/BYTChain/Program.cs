using System;

// Handler interface
public interface IOperationHandler
{
    void SetNextHandler(IOperationHandler nextHandler);
    void HandleRequest(OperationRequest request);
}

// Handler for basic operations
public class BasicArithmeticHandler : IOperationHandler
{
    private IOperationHandler nextHandler;

    public void SetNextHandler(IOperationHandler nextHandler)
    {
        this.nextHandler = nextHandler;
    }

    public void HandleRequest(OperationRequest request)
    {
        switch (request.Operation)
        {
            case Operation.Add:
                Console.WriteLine($"{request.num1} + {request.num2} = {request.num1 + request.num2}");
                break;
            case Operation.Subtract:
                Console.WriteLine($"{request.num1} - {request.num2} = {request.num1 - request.num2}");
                break;
            case Operation.Multiply:
                Console.WriteLine($"{request.num1} * {request.num2} = {request.num1 * request.num2}");
                break;
            case Operation.Divide:
                if (request.num2 != 0)
                {
                    Console.WriteLine($"{request.num1} / {request.num2} = {request.num1 / request.num2}");
                }
                else
                {
                    Console.WriteLine("Cannot divide by zero.");
                }
                break;
            default:
                // Pass it to next handler
                nextHandler?.HandleRequest(request);
                break;
        }
    }
}

public enum Operation
{
    Add,
    Subtract,
    Multiply,
    Divide
}

public class OperationRequest
{
    public double num1 { get; set; }
    public double num2 { get; set; }
    public Operation Operation { get; set; }
}

class Program
{
    static void Main()
    {
        IOperationHandler arithmeticHandler = new BasicArithmeticHandler();

        OperationRequest req1 = new OperationRequest { num1 = 10, num2 = 5, Operation = Operation.Add };
        OperationRequest req2 = new OperationRequest { num1 = 10, num2 = 5, Operation = Operation.Multiply };
        OperationRequest req3 = new OperationRequest { num1 = 10, num2 = 0, Operation = Operation.Divide };
        OperationRequest req4 = new OperationRequest { num1 = 8, num2 = 3, Operation = Operation.Subtract };

        arithmeticHandler.HandleRequest(req1);
        arithmeticHandler.HandleRequest(req2);
        arithmeticHandler.HandleRequest(req3);
        arithmeticHandler.HandleRequest(req4);
    }
}
