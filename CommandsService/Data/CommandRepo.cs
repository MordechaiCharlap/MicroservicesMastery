using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateCommand(int platformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            else
            {

                var platform = _context.Platforms.FirstOrDefault(p => p.Id == platformId);
                if (platform == null)
                {
                    throw new ArgumentException("This platform does not exist");
                }
                else
                {

                    command.PlatformId = platformId;
                    _context.Commands.Add(command);
                }

            }


        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            else
            {
                _context.Platforms.Add(platform);
            }
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms;
        }

        public Command GetCommand(int platformId, int commandId)
        {
            var command = _context.Commands
            .Where(c => c.Id == commandId && c.PlatformId == platformId)
            .FirstOrDefault();

            return command;
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands
                .Where(c => c.PlatformId == platformId)
                .ToList();
        }

        public bool PlatformExist(int platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}