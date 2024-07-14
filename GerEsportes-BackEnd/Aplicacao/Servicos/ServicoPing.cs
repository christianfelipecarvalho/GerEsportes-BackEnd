using GerEsportes_BackEnd.Repositorios.Pings;

namespace GerEsportes_BackEnd.Aplicacao.Servicos
{
    public class ServicoPing : BackgroundService
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ServicoPing> _logger;


        public ServicoPing(IServiceProvider serviceProvider, ILogger<ServicoPing> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Coloque sua lógica de tarefa agendada aqui
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetRequiredService<IRepPing>();
                        // Substitua o ID "1" pelo ID desejado
                        var ping = repository.RecuperarPorId(1);
                        if (ping != null)
                        {
                            // Faça algo com o ping, se necessário
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Lidar com exceções
                    _logger.LogError(ex, "Ocorreu um erro ao executar a tarefa agendada.");
                }

                // Aguarde o atraso especificado antes de executar novamente
                await Task.Delay(15000, stoppingToken); // Atraso de 15 segundos
            }
        }
    }
}
