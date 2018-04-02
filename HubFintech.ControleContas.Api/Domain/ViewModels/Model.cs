using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json;

namespace HubFintech.ControleContas.Api.Domain.ViewModels
{
    public class Model<T> : Model where T : new()
    {
        public T Retorno { get; set; }

        public Model() : this(HttpStatusCode.OK)
        {
        }

        public Model(HttpStatusCode httpStatusCode)
            : base(httpStatusCode)
        {
            Retorno = new T();
        }
    }

    public class Model
    {
        private List<Mensagem> _erros;

        public Model(HttpStatusCode httpStatusCode)
        {
            if (httpStatusCode == default(HttpStatusCode))
            {
                httpStatusCode = HttpStatusCode.OK;
            }

            StatusCode = httpStatusCode;
        }

        public Model(ModelStateDictionary modelState)
        {
            _erros = modelState.Keys
                .SelectMany(key =>
                    modelState[key].Errors.Select(x =>
                        string.IsNullOrEmpty(x.ErrorMessage)
                            ? new Mensagem(((int) ViewModels.Erro.ErroNaoTratado).ToString(), x.Exception.Message,
                                null)
                            : ValidationFailedResult.ToErrorObject(x.ErrorMessage)))
                .ToList();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Mensagem> Erros
        {
            get => _erros;
        }

        [JsonIgnore] public HttpStatusCode StatusCode { get; private set; }

        public void TransferirRetorno(Model model)
        {
            if (model.IsSucess())
                return;

            StatusCode = model.StatusCode;
            model.Erros.ToList().ForEach(AdicionarErro);
        }

        public void Erro(Exception exception)
        {
            switch (exception)
            {
                case TaskCanceledException taskCanceledException:
                    Erro(taskCanceledException, HttpStatusCode.BadGateway);
                    break;
                default:
                    Erro(exception, HttpStatusCode.InternalServerError);
                    break;
            }
        }

        public void Erro(Exception exception, HttpStatusCode statusCode)
        {
            if (exception == null)
                throw new InvalidOperationException("Uma exception deve ser infomarda nesse método.");

            StatusCode = statusCode;

            var existentes = Erros as List<Mensagem> ?? new List<Mensagem>();

            existentes.Add(new Mensagem(string.Empty, exception.Message));

            _erros = existentes;
        }

        public bool IsSucess()
        {
            return IsSuccessStatusCode() || IsRedirectionStatusCode() || IsInformationalStatusCode();
        }

        public bool IsError()
        {
            return IsServerErrorStatusCode() || IsClientErrorStatusCode();
        }

        private void AdicionarErro(Mensagem erro)
        {
            if (erro == null)
                throw new ArgumentNullException(nameof(erro));

            if (_erros == null)
                _erros = new List<Mensagem>();

            if (_erros.Any(x =>
                x.Codigo == erro.Codigo &&
                string.Equals(x.Descricao, erro.Descricao, StringComparison.InvariantCultureIgnoreCase)))
                RemoverErro(erro);

            _erros.Add(erro);
        }

        private void RemoverErro(Mensagem erro)
        {
            if (erro == null)
                throw new ArgumentNullException(nameof(erro));

            if (_erros == null)
                _erros = new List<Mensagem>();

            var erroAntigo =
                _erros.FirstOrDefault(x =>
                    x.Codigo == erro.Codigo &&
                    string.Equals(x.Descricao, erro.Descricao, StringComparison.InvariantCultureIgnoreCase));

            if (erroAntigo == null)
                throw new ArgumentOutOfRangeException(nameof(erroAntigo));

            _erros.Remove(erro);
        }

        private bool IsInformationalStatusCode()
        {
            return ((int) StatusCode >= 100 && (int) StatusCode <= 199);
        }

        private bool IsSuccessStatusCode()
        {
            return ((int) StatusCode >= 200 && (int) StatusCode <= 299);
        }

        private bool IsRedirectionStatusCode()
        {
            return ((int) StatusCode >= 300 && (int) StatusCode <= 399);
        }

        private bool IsClientErrorStatusCode()
        {
            return ((int) StatusCode >= 300 && (int) StatusCode <= 499);
        }

        private bool IsServerErrorStatusCode()
        {
            return ((int) StatusCode >= 500 && (int) StatusCode <= 599);
        }
    }

    public class Mensagem
    {
        public Mensagem()
        {
        }

        public Mensagem(string propriedade, string descricao)
        {
            Propriedade = propriedade != string.Empty ? propriedade : null;
            Descricao = descricao;
        }

        public Mensagem(string codigo, string descricao, object valorInformado)
            : this(string.Empty, descricao)
        {
            Codigo = codigo != string.Empty ? codigo : null;
            ValorInformado = valorInformado;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Propriedade { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Codigo { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Descricao { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Object ValorInformado { get; set; }
    }
}