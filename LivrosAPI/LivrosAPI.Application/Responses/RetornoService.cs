using Flunt.Notifications;
using LivrosAPI.Domain.Constants;

namespace LivrosAPI.Application.Responses
{
    public class RetornoService
    {
        public bool Sucesso { get; set; }
        public int IdEntidade { get; set; }

        public IList<Notification> ListaMensagem { get; } = new List<Notification>();

        public bool TemMensagens => ListaMensagem.Any();

        public object Value { get; private set; }
        public byte[] ValueFile { get; private set; }

        /// <summary>
        /// Cria um novo objeto de retorno para a api
        /// </summary>
        public RetornoService()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="objeto">Objeto que deverá ser serializado pela Api</param>
        public RetornoService(object objeto) : this()
        {
            AddValue(objeto);
        }

        /// <summary>
        /// Adiciona um objeto que deverá ser serializado e retornado pela Api
        /// </summary>
        /// <param name="objeto">Objeto que deverá ser serializado pela Api</param>
        public void AddValue(object objeto)
        {
            Value = objeto;
        }

        public void AddValue(byte[] file)
        {
            ValueFile = file;
        }

        /// <summary>
        /// Adiciona mensagem de retorno
        /// </summary>
        /// <param name="notification">Mensagem que deverá ser retornada pela Api</param>
        public void AddNotification(Notification notification)
        {
            ListaMensagem.Add(notification);
        }

        /// <summary>
        /// Adiciona mensagem de retorno
        /// </summary>
        /// <param name="notification">Mensagem que deverá ser retornada pela Api</param>
        public void AddErrorNotification(string message, Exception ex)
        {
            Notification notification = new Notification(message, ex.Message);

            Sucesso = false;
            ListaMensagem.Add(notification);
        }

        /// <summary>
        /// Adiciona mensagem de retorno
        /// </summary>
        /// <param name="notification">Mensagem que deverá ser retornada pela Api</param>
        public void AddErrorNotification(string message)
        {
            Notification notification = new Notification("Erro", message);

            Sucesso = false;
            ListaMensagem.Add(notification);
        }

        /// <summary>
        /// Adiciona mensagem de retorno
        /// </summary>
        /// <param name="notification">Mensagem que deverá ser retornada pela Api</param>
        public void AddErrorSemPermissaoNotification()
        {
            AddErrorNotification(Constants.Messages.SEM_PERMISSAO);
        }

        /// <summary>
        /// Adiciona mensagens de retorno
        /// </summary>
        /// <param name="notifications">Notificações</param>
        /// <param name="type">Tipo de notificação</param>
        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                AddNotification(notification);
            }
        }

        public string GetListaMensagemToString()
        {
            return string.Join(" - ", ListaMensagem.Select(x => x.Message));
        }

        /// <summary>
        /// Re
        /// </summary>
        /// <param name="notifications">Notificações</param>
        /// <param name="type">Tipo de notificação</param>
        public static RetornoService AcessoNegado()
        {
            var notification = new Notification("Messagem", "Acesso negado");
            RetornoService acessonegado = new RetornoService();
            acessonegado.Sucesso = false;
            acessonegado.AddNotification(notification);
            return acessonegado;
        }

        public bool EmailValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
