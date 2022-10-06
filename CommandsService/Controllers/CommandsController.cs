
using System;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> Getting Commands from Platform number {platformId} from CommandsService");
            if (_repository.PlatformExist(platformId))
            {
                var commandItems = _repository.GetCommandsForPlatform(platformId);
                return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
            }
            return NotFound();
        }
        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Getting Command number {commandId} from Platform number {platformId} from CommandsService");

            var command = _mapper.Map<CommandReadDto>(_repository.GetCommand(platformId, commandId));

            if (command == null) return NotFound();

            return Ok(_mapper.Map<CommandReadDto>(command));
        }
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            Console.WriteLine($"--> Creating Command for Platform number {platformId} from CommandsService");

            if (_repository.PlatformExist(platformId))
            {
                var command = _mapper.Map<Command>(commandDto);

                _repository.CreateCommand(platformId, command);
                _repository.SaveChanges();

                var commandReadDto = _mapper.Map<CommandReadDto>(command);

                return CreatedAtRoute(nameof(GetCommandForPlatform),
                new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
            }
            else
            {
                return NotFound();
            }


        }
    }
}