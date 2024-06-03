﻿namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : SystemException
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key} was not found.")
        {

        }
    }
}
