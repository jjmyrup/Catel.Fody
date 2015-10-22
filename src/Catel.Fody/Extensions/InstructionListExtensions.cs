﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InstructionListExtensions.cs" company="Catel development team">
//   Copyright (c) 2008 - 2013 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Catel.Fody
{
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using Mono.Collections.Generic;

    public static class InstructionListExtensions
    {
        public static SequencePoint GetFirstSequencePoint(this IEnumerable<Instruction> instructions)
        {
            return instructions.Select(x => x.SequencePoint).FirstOrDefault(y => y != null);
        }

        public static SequencePoint GetSequencePoint(this IList<Instruction> instructions, Instruction instruction)
        {
            var index = instructions.IndexOf(instruction);
            if (index < 0)
            {
                return null;
            }

            for (int i = index; i >= 0; i--)
            {
                var ix = instructions[i];
                if (ix.SequencePoint != null)
                {
                    return ix.SequencePoint;
                }
            }

            return null;
        }

        public static Instruction GetPreviousInstruction(this IList<Instruction> instructions, Instruction instruction)
        {
            var currentIndex = instructions.IndexOf(instruction);
            if (currentIndex <= 0)
            {
                return null;
            }

            return instructions[currentIndex - 1];
        }

        public static bool UsesType(this IList<Instruction> instructions, TypeDefinition typeDefinition, params OpCode[] opCodes)
        {
            for (var index = 0; index < instructions.Count; index++)
            {
                var instruction = instructions[index];
                if (instruction.UsesType(typeDefinition, opCodes))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool UsesField(this IList<Instruction> instructions, FieldDefinition field)
        {
            for (var index = 0; index < instructions.Count; index++)
            {
                var instruction = instructions[index];
                if (instruction.UsesField(field))
                {
                    return true;
                }
            }

            return false;
        }

        public static void Prepend(this Collection<Instruction> collection, params Instruction[] instructions)
        {
            for (var index = 0; index < instructions.Length; index++)
            {
                var instruction = instructions[index];
                collection.Insert(index, instruction);
            }
        }

        public static void Append(this Collection<Instruction> collection, params Instruction[] instructions)
        {
            for (var index = 0; index < instructions.Length; index++)
            {
                collection.Insert(index, instructions[index]);
            }
        }

        public static void BeforeLast(this Collection<Instruction> collection, params Instruction[] instructions)
        {
            var index = collection.Count - 1;
            foreach (var instruction in instructions)
            {
                collection.Insert(index, instruction);
                index++;
            }
        }

        public static int Insert(this Collection<Instruction> collection, int index, List<Instruction> instructions)
        {
            return Insert(collection, index, instructions.ToArray());
        }

        public static int Insert(this Collection<Instruction> collection, int index, params Instruction[] instructions)
        {
            foreach (var instruction in instructions.Reverse())
            {
                collection.Insert(index, instruction);
            }
            return index + instructions.Length;
        }

        //public static void RemoveSubsequentNops(this Collection<Instruction> collection)
        //{
        //    var previousWasNop = false;
        //    for (var i = 0; i < collection.Count; i++)
        //    {
        //        var instruction = collection[i];
        //        if (instruction.OpCode == OpCodes.Nop)
        //        {
        //            if (!previousWasNop)
        //            {
        //                previousWasNop = true;
        //            }
        //            else
        //            {
        //                collection.RemoveAt(i--);
        //            }
        //        }
        //        else
        //        {
        //            previousWasNop = false;
        //        }
        //    }
        //}

        //public static void FixWrongBranchOutOfMethods(this Collection<Instruction> instructions)
        //{
        //    var lastReturn = instructions.Last(x => x.OpCode == OpCodes.Ret);

        //    foreach (var instruction in instructions)
        //    {
        //        if (instruction.OpCode == OpCodes.Brtrue_S || instruction.OpCode == OpCodes.Brtrue ||
        //            instruction.OpCode == OpCodes.Brfalse || instruction.OpCode == OpCodes.Brfalse_S)
        //        {
        //            var targetInstruction = instruction.Operand as Instruction;
        //            if (!instructions.Contains(targetInstruction))
        //            {
        //                instruction.Operand = lastReturn;
        //            }
        //        }
        //    }
        //}
    }
}