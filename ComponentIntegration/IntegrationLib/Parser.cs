using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IntegrationLib
{
    public class Parser : Component
    {
        private abstract class Item
        {
        }
        private abstract class Operation : Item
        {
            public virtual int Priority
            {
                get;
                protected set;
            }
        }

        private class Brackets : Operation
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
        private class BinaryOperation : Operation
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

            public Item LeftItem
            {
                get;
                set;
            }
            public Item RightItem
            {
                get;
                set;
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
        }
        private class UnaryOperation : Operation
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
        }

        private class Variable : Item
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
        }
        private class Number : Item
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
        }

        public interface IManyArgumentsFunction
        {
            double GetValue(Dictionary<char, double> arguments);
        }

        private class ParserResult : IManyArgumentsFunction
        {
            public Stack<Item> Function
            {
                get;
                private set;
            }

            public ParserResult(Stack<Item> function)
            {
                Function = function;
            }

            public double GetValue(Dictionary<char, double> arguments)
            {
                Stack<double> result = new Stack<double>();
                Item[] elements = new Item[Function.Count];
                Function.CopyTo(elements, 0);

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
                            throw new ArgumentException("Variable "+ ((Variable)element).Name +" is not found in dictionary!");
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

        public IManyArgumentsFunction ParseString(String input)
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

            return new ParserResult(result);
        }
    }
}