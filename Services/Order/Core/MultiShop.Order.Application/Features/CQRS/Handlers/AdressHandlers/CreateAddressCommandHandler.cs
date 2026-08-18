using FluentValidation;
using MultiShop.Order.Application.Features.CQRS.Commands.AdressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers
{
    public class CreateAddressCommandHandler
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IValidator<CreateAddressCommand> _validator;

        public CreateAddressCommandHandler(IRepository<Address> addressRepository, IValidator<CreateAddressCommand> validator)
        {
            _addressRepository = addressRepository;
            _validator = validator;
        }
        public async Task HandleAsync(CreateAddressCommand createAddressCommand)
        {
            await _validator.ValidateAndThrowAsync(createAddressCommand);
            await _addressRepository.CreateAsync(new Address
            {
                City = createAddressCommand.City,
                Detail1 = createAddressCommand.Detail1,
                Detail2 = createAddressCommand.Detail2,
                District = createAddressCommand.District,
                UserId = createAddressCommand.UserId,
                Country = createAddressCommand.Country,
                Description = createAddressCommand.Description,
                PhoneNo = createAddressCommand.PhoneNo,
                Email = createAddressCommand.Email,
                Name = createAddressCommand.Name,
                Surname = createAddressCommand.Surname,
                ZipCode = createAddressCommand.ZipCode,
            });
        }
    }
}
