using FluentValidation;
using MultiShop.Order.Application.Features.CQRS.Commands.AdressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers
{
    public class UpdateAddressCommandHandler
    {
        private readonly IRepository<Address> _repository;
        private readonly IValidator<UpdateAddressCommand> _validator;

        public UpdateAddressCommandHandler(IRepository<Address> repository, IValidator<UpdateAddressCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task HandlerAsync(UpdateAddressCommand command)
        {
            await _validator.ValidateAndThrowAsync(command);
            var adress = await _repository.GetByIdAsync(command.AddressId);
            adress.UserId = command.UserId;
            adress.District = command.District;
            adress.City = command.City;
            adress.Detail1 = command.Detail1;
            adress.Detail2 = command.Detail2;
            adress.Description = command.Description;
            adress.Country = command.Country;
            adress.PhoneNo = command.PhoneNo;
            adress.Email = command.Email;
            adress.Name = command.Name;
            adress.Surname = command.Surname;
            adress.ZipCode = command.ZipCode;
            await _repository.UpdateAsync(adress);
        }
    }
}
