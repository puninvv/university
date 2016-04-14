using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IntegrationLib
{
    public class Derivator : Component
    {
        public class Parser
        {
            public abstract class Item
            {
            }
            public abstract class Operation : Item
            {
                public virtual int Priority
                {
                    get;
                    protected set;
                }
            }

            public class Brackets : Operation
            {
                public enum BracketsType
                {
                    Open, Close
                }

                public BracketsType Type
                {
                    get;
                    private set;
                }

                public Brackets(BracketsType type)
                {
                    Type = type;
                    Priority = 1;
                }
            }
            public class BinaryOperation : Operation
            {
                public enum BinaryOperationType
                {
                    plus, minus, multiply, divide, power
                }

                public BinaryOperationType Type
                {
                    get;
                    private set;
                }
                
                public BinaryOperation(BinaryOperationType type)
                {
                    Type = type;
                    switch (type)
                    {
                        case BinaryOperationType.power:
                            Priority = 5;
                            break;
                        case BinaryOperationType.multiply:
                            Priority = 3;
                            break;
                        case BinaryOperationType.divide:
                            Priority = 3;
                            break;
                        case BinaryOperationType.plus:
                            Priority = 2;
                            break;
                        case BinaryOperationType.minus:
                            Priority = 2;
                            break;
                    }
                }

                public override string ToString()
                {
                    switch (Type)
                    {
                        case BinaryOperationType.power:
                            return "^";
                        case BinaryOperationType.multiply:
                            return "*";
                        case BinaryOperationType.divide:
                            return "/";
                        case BinaryOperationType.plus:
                            return "+";
                        case BinaryOperationType.minus:
                            return "-";
                    }
                    return "";
                }
            }
            public class UnaryOperation : Operation
            {
                public enum UnaryOperationType
                {
                    sin, cos, log
                }

                public UnaryOperationType Type
                {
                    get;
                    private set;
                }

                public UnaryOperation(UnaryOperationType type)
                {
                    Type = type;
                    switch (type)
                    {
                        case UnaryOperationType.cos:
                            Priority = 4;
                            break;
                        case UnaryOperationType.sin:
                            Priority = 4;
                            break;
                        case UnaryOperationType.log:
                            Priority = 4;
                            break;
                    }
                }

                public override string ToString()
                {
                    switch (Type)
                    {
                        case UnaryOperationType.cos:
                            return "cos";
                        case UnaryOperationType.sin:
                            return "sin";
                        case UnaryOperationType.log:
                            return "log";
                    }
                    return "";
                }
            }

            public class Variable : Item
            {
                public Char Name
                {
                    get;
                    private set;
                }
                public Variable(Char name)
                {
                    Name = name;
                }

                public override string ToString()
                {
                    return Name.ToString();
                }
            }
            public class Number : Item
            {
                public double Value
                {
                    get;
                    private set;
                }

                public Number(double value)
                {
                    Value = value;
                }

                public static Number TryParse(String s, ref int start_pos)
                {
                    int end_pos = start_pos;
                    while (end_pos < s.Length && ((s[end_pos] >= '0' && s[end_pos] <= '9') || s[end_pos] == ','))
                        end_pos++;

                    String tmp = s.Substring(start_pos, end_pos - start_pos);
                    double value = Double.Parse(tmp);

                    start_pos = end_pos;
                    return new Number(value);
                }

                public override string ToString()
                {
                    return Value.ToString();
                }
            }

            public static Stack<Item> ParseString(String input)
            {
                Stack<Item> result = new Stack<Item>();
                Stack<Operation> operations = new Stack<Operation>();

                for (int i = 0; i < input.Length; i++)
                {
                    Char now = input[i];
                    if (now == ' ')
                        continue;
                    else
                    if (now >= '0' && now <= '9')
                    {
                        result.Push(Number.TryParse(input, ref i));
                        i--;
                    }
                    else
                    if (now == '(')
                        operations.Push(new Brackets(Brackets.BracketsType.Open));
                    else
                    if (now == ')')
                    {
                        while (operations.Count != 0)
                        {
                            Operation operation = operations.Peek();
                            if (operation is Brackets && ((Brackets)operation).Type == Brackets.BracketsType.Open)
                            {
                                operations.Pop();
                                break;
                            }
                            else
                                result.Push(operations.Pop());
                        }
                    }
                    else
                    if (now == 's')
                    {
                        while (operations.Count != 0 && operations.Peek().Priority >= 4)
                            result.Push(operations.Pop());
                        operations.Push(new UnaryOperation(UnaryOperation.UnaryOperationType.sin));
                        i += 2;
                    }
                    else
                    if (now == 'c')
                    {
                        while (operations.Count != 0 && operations.Peek().Priority >= 4)
                            result.Push(operations.Pop());
                        operations.Push(new UnaryOperation(UnaryOperation.UnaryOperationType.cos));
                        i += 2;
                    }
                    else
                    if (now == 'l')
                    {
                        while (operations.Count != 0 && operations.Peek().Priority >= 4)
                            result.Push(operations.Pop());
                        operations.Push(new UnaryOperation(UnaryOperation.UnaryOperationType.log));
                        i += 1;
                    }
                    else
                    if (now == '^')
                    {
                        while (operations.Count != 0 && operations.Peek().Priority >= 5)
                            result.Push(operations.Pop());
                        operations.Push(new BinaryOperation(BinaryOperation.BinaryOperationType.power));
                    }
                    else
                    if (now == '*')
                    {
                        while (operations.Count != 0 && operations.Peek().Priority >= 3)
                            result.Push(operations.Pop());
                        operations.Push(new BinaryOperation(BinaryOperation.BinaryOperationType.multiply));
                    }
                    else
                    if (now == '/')
                    {
                        while (operations.Count != 0 && operations.Peek().Priority >= 3)
                            result.Push(operations.Pop());
                        operations.Push(new BinaryOperation(BinaryOperation.BinaryOperationType.divide));
                    }
                    else
                    if (now == '+')
                    {
                        while (operations.Count != 0 && operations.Peek().Priority >= 2)
                            result.Push(operations.Pop());
                        operations.Push(new BinaryOperation(BinaryOperation.BinaryOperationType.plus));
                    }
                    else
                    if (now == '-')
                    {
                        while (operations.Count != 0 && operations.Peek().Priority >= 2)
                            result.Push(operations.Pop());
                        operations.Push(new BinaryOperation(BinaryOperation.BinaryOperationType.minus));
                    }
                    else
                    if (now >= 'a' && now <= 'z')
                        result.Push(new Variable(now));
                }

                while (operations.Count != 0)
                    result.Push(operations.Pop());

                return result;
            }
            public static double GetValue(Stack<Item> items, Dictionary<char, double> arguments)
            {
                Stack<double> result = new Stack<double>();
                Item[] elements = new Item[items.Count];
                items.CopyTo(elements, 0);

                for (int i = elements.Length - 1; i >= 0; i--)
                {
                    Item element = elements[i];
                    if (element is Number)
                        result.Push(((Number)element).Value);
                    else
                    if (element is Variable)
                    {
                        double value = 0;

                        if (arguments == null)
                            throw new ArgumentException("Variable " + ((Variable)element).Name + " is not found in dictionary!");

                        if (arguments.TryGetValue(((Variable)element).Name, out value))
                            result.Push(value);
                        else
                            throw new ArgumentException("Variable " + ((Variable)element).Name + " is not found in dictionary!");
                    }
                    else
                    {
                        if (element is BinaryOperation)
                        {
                            BinaryOperation operation = (BinaryOperation)element;
                            if (operation.Type == BinaryOperation.BinaryOperationType.divide)
                            {
                                double divider = result.Pop();
                                result.Push(result.Pop() / divider);
                            }
                            else
                            if (operation.Type == BinaryOperation.BinaryOperationType.multiply)
                                result.Push(result.Pop() * result.Pop());
                            else
                            if (operation.Type == BinaryOperation.BinaryOperationType.plus)
                            {
                                double value = result.Pop();
                                if (result.Count == 0)
                                    result.Push(value);
                                else
                                    result.Push(result.Pop() + value);
                            }
                            else
                                if (operation.Type == BinaryOperation.BinaryOperationType.minus)
                            {
                                double value = result.Pop();
                                if (result.Count == 0)
                                    result.Push(-value);
                                else
                                    result.Push(result.Pop() - value);
                            }
                            else
                                if (operation.Type == BinaryOperation.BinaryOperationType.power)
                            {
                                double pow = result.Pop();
                                result.Push(Math.Pow(result.Pop(), pow));
                            }
                        }
                        else
                        if (element is UnaryOperation)
                        {
                            UnaryOperation operation = (UnaryOperation)element;
                            if (operation.Type == UnaryOperation.UnaryOperationType.cos)
                                result.Push(Math.Cos(result.Pop()));
                            else
                                if (operation.Type == UnaryOperation.UnaryOperationType.sin)
                                result.Push(Math.Sin(result.Pop()));
                            else
                                if (operation.Type == UnaryOperation.UnaryOperationType.log)
                                result.Push(Math.Log(result.Pop()));
                        }
                    }
                }

                return result.Pop();
            }
        }

        public class SyntaxTree
        {
            private class SyntaxTreeNode
            {
                private Parser.Item Type
                {
                    get;
                    set;
                }
                private SyntaxTreeNode LeftNode;
                private SyntaxTreeNode RightNode;

                public SyntaxTreeNode(Parser.Item type)
                {
                    Type = type;
                }
                private SyntaxTreeNode()
                {
                }

                public SyntaxTreeNode(Stack<Parser.Item> elements)
                {
                    Type = elements.Pop();
                    if (Type is Parser.Operation)
                    {
                        if (Type is Parser.BinaryOperation)
                            RightNode = new SyntaxTreeNode(elements);
                        LeftNode = new SyntaxTreeNode(elements);
                    }
                }

                private void ToStack(Stack<Parser.Item> elements)
                {
                    if (Type is Parser.Operation)
                    {
                        LeftNode.ToStack(elements);
                        if (Type is Parser.BinaryOperation)
                            RightNode.ToStack(elements);
                    }

                    elements.Push(Type);
                }
                public Stack<Parser.Item> ToStack()
                {
                    Stack<Parser.Item> result = new Stack<Parser.Item>();
                    ToStack(result);
                    return result;
                }


                public SyntaxTreeNode Derivate(Char variable)
                {
                    SyntaxTreeNode result = new SyntaxTreeNode(new Parser.Number(0.0));

                    if (Type is Parser.Variable && ((Parser.Variable)Type).Name == variable)
                        result = new SyntaxTreeNode(new Parser.Number(1.0));
                    else
                    if (Type is Parser.BinaryOperation)
                    {
                        SyntaxTreeNode leftDerivative = LeftNode.Derivate(variable);
                        SyntaxTreeNode rightDerivative = RightNode.Derivate(variable);

                        if (((Parser.BinaryOperation)Type).Type == Parser.BinaryOperation.BinaryOperationType.plus)
                        {
                            result = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.plus));
                            result.LeftNode = leftDerivative;
                            result.RightNode = rightDerivative;
                        }
                        else
                        if (((Parser.BinaryOperation)Type).Type == Parser.BinaryOperation.BinaryOperationType.minus)
                        {
                            result = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.minus));
                            result.LeftNode = leftDerivative;
                            result.RightNode = rightDerivative;
                        }
                        else
                        if (((Parser.BinaryOperation)Type).Type == Parser.BinaryOperation.BinaryOperationType.multiply)
                        {
                            result = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.plus));
                            result.LeftNode = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.multiply));
                            result.RightNode = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.multiply));

                            result.LeftNode.LeftNode = leftDerivative;
                            result.LeftNode.RightNode = RightNode;

                            result.RightNode.LeftNode = LeftNode;
                            result.RightNode.RightNode = rightDerivative;
                        }
                        else
                        if (((Parser.BinaryOperation)Type).Type == Parser.BinaryOperation.BinaryOperationType.divide)
                        {
                            result = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.divide));

                            result.LeftNode = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.minus));
                            result.LeftNode.LeftNode = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.multiply));
                            result.LeftNode.LeftNode.LeftNode = leftDerivative;
                            result.LeftNode.LeftNode.RightNode = RightNode;

                            result.LeftNode.RightNode = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.multiply));
                            result.LeftNode.RightNode.LeftNode = rightDerivative;
                            result.LeftNode.RightNode.RightNode = LeftNode;

                            result.RightNode = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.multiply));
                            result.RightNode.LeftNode = RightNode;
                            result.RightNode.RightNode = RightNode;
                        }
                        else
                        if (((Parser.BinaryOperation)Type).Type == Parser.BinaryOperation.BinaryOperationType.power)
                        {
                            if (RightNode.Type is Parser.Number)
                            {
                                result = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.multiply));

                                result.LeftNode = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.multiply));
                                result.LeftNode.LeftNode = RightNode;
                                result.LeftNode.RightNode = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.power));
                                result.LeftNode.RightNode.LeftNode = LeftNode;
                                result.LeftNode.RightNode.RightNode = new SyntaxTreeNode(new Parser.Number(((Parser.Number)(RightNode.Type)).Value - 1));

                                result.RightNode = LeftNode.Derivate(variable);
                            }
                            else
                                throw new Exception("Мне не нужна такая производная");
                        }

                    }
                    else
                    if (Type is Parser.UnaryOperation)
                    {
                        result = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.multiply));
                        result.RightNode = LeftNode.Derivate(variable);

                        if (((Parser.UnaryOperation)Type).Type == Parser.UnaryOperation.UnaryOperationType.cos)
                        {
                            result.LeftNode = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.minus));
                            result.LeftNode.LeftNode = new SyntaxTreeNode(new Parser.Number(0.0));
                            result.LeftNode.RightNode = new SyntaxTreeNode(new Parser.UnaryOperation(Parser.UnaryOperation.UnaryOperationType.sin));
                            result.LeftNode.RightNode.LeftNode = LeftNode;
                        }
                        else
                        if (((Parser.UnaryOperation)Type).Type == Parser.UnaryOperation.UnaryOperationType.sin)
                        {
                            result.LeftNode = new SyntaxTreeNode(new Parser.UnaryOperation(Parser.UnaryOperation.UnaryOperationType.cos));
                            result.LeftNode.LeftNode = LeftNode;
                        }
                        else
                        if (((Parser.UnaryOperation)Type).Type == Parser.UnaryOperation.UnaryOperationType.log)
                        {
                            result.LeftNode = new SyntaxTreeNode(new Parser.BinaryOperation(Parser.BinaryOperation.BinaryOperationType.divide));

                            result.LeftNode.LeftNode = new SyntaxTreeNode(new Parser.Number(1.0));
                            result.LeftNode.RightNode = LeftNode;
                        }
                    }

                    return result;
                }
                
            }
            
            private SyntaxTreeNode Root
            {
                get;
                set;
            }

            public SyntaxTree(Stack<Parser.Item> items)
            {
                Root = new SyntaxTreeNode(items);
            }
            private SyntaxTree(){ }

            public Stack<Parser.Item> ToStack()
            {
                return Root.ToStack();
            }

            public SyntaxTree Derivate(Char variable)
            {
                SyntaxTree result = new SyntaxTree();
                result.Root = Root.Derivate(variable);
                if (result.Root == null)
                    result.Root = new SyntaxTreeNode(new Parser.Number(0.0));

                return result;
            }
        }

        public interface IManyArgumentsDerivative
        {
            double GetValue(Dictionary<Char, Double> arguments);
        }

        private class DerivatorResult : IManyArgumentsDerivative
        {
            private SyntaxTree Tree
            {
                get;
                set;
            }

            private Stack<Parser.Item> Items;

            public DerivatorResult(SyntaxTree tree)
            {
                Tree = tree;
                Items = Tree.ToStack();
            }

            public double GetValue(Dictionary<char, double> arguments)
            {
                return Parser.GetValue(Items, arguments);
            }
        }

        public IManyArgumentsDerivative Derivate(String input, Char variable, int n = 1)
        {
            SyntaxTree tree = new SyntaxTree(Parser.ParseString(input));

            for (int i = 0; i < n; i++)
                tree = tree.Derivate(variable);

            return new DerivatorResult(tree);
        }
    }
}
