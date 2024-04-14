using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QualiCadastro.Models;
using QualiCadastro.Services;

namespace QualiCadastro.Controllers
{

    [Route("/Cadastros")]
    public class CadastroController : Controller
	{
		private readonly ApplicationDbContext context;
        private static List<Email> _emails = new List<Email>();

		public CadastroController(ApplicationDbContext context)
        {
			this.context = context;
        }

        public IActionResult Index()
		{
			return View(context.Cadastros.OrderBy(p => p.Id).ToList());
		}

        [HttpGet("Cadastrar")]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost("Cadastrar")]
        public IActionResult Cadastrar(Cadastro cadastro)
        {
            cadastro.Id = 0;

            if (cadastro.Nome.IsNullOrEmpty())
            {
                ModelState.AddModelError("Nome", "Nome é um campo obrigatório");

                return View(cadastro);
            }

            context.Cadastros.Add(cadastro);
            context.SaveChanges();

            int idUltimoCadastro = context.Cadastros.OrderByDescending(p => p.Id).First().Id;

            foreach (var email in _emails)
            {
                email.IdCadastro = idUltimoCadastro;
                email.Cadastro = cadastro;
                context.Emails.Add(email);
            }

            context.SaveChanges();

            _emails = new List<Email>();

            return RedirectToAction("Index", "Cadastros");
        }

        [HttpGet("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            var cadastro = context.Cadastros.Find(id);
            if (cadastro == null)
            {
                return RedirectToAction("Index", "Cadastros");
            }

            ViewData["CadastroId"] = cadastro.Id;

            return View(cadastro);
        }

        [HttpPost("Editar/{id}")]
        public IActionResult Editar(int id, Cadastro cadastro)
        {
            cadastro = context.Cadastros.Find(id);
            if (cadastro == null)
            {
                return RedirectToAction("Index", "Cadastros");
            }

            Cadastro novoCadastro = new Cadastro();

            novoCadastro.Id = id;
            novoCadastro.Nome = cadastro.Nome;
            novoCadastro.Empresa = cadastro.Empresa;
            novoCadastro.TelefonePessoal = cadastro.TelefonePessoal;
            novoCadastro.TelefoneComercial = cadastro.TelefoneComercial;
            novoCadastro.Emails = cadastro.Emails;

            context.Cadastros.Update(novoCadastro);
            context.SaveChanges();

            _emails = new List<Email>();

            return RedirectToAction("Index", "Cadastros");
        }

        [HttpGet("Apagar/{id}")]
        public IActionResult Apagar(int id)
        {
            var cadastro = context.Cadastros.Find(id);
            if (cadastro == null)
            {
                return RedirectToAction("Index", "Cadastros");
            }

            context.Cadastros.Remove(cadastro);
            context.SaveChanges(true);

            return RedirectToAction("Index", "Cadastros");
        }

        [HttpGet("Email/{id}")]
        public IActionResult Email(int id)
        {
            var email = context.Emails.Where(x => x.IdCadastro == id).ToList();
            if (email == null)
            {
                return RedirectToAction("Index", "Cadastros");
            }

            return View(email);
        }

        [HttpGet("EmailCadastrar")]
        public IActionResult EmailCadastrar()
        {
            return View(new CadastroEmail { Emails = _emails });
        }

        [HttpPost("EmailCadastrar")]
        public IActionResult EmailCadastrar(CadastroEmail cadastroEmail)
        {
            if (!cadastroEmail.EmailTexto.IsNullOrEmpty())
            {
                Email email = new Email { EmailCadastro = cadastroEmail.EmailTexto };

                _emails.Add(email);
                cadastroEmail.Emails = _emails;
            }

            return View(cadastroEmail);
        }
    }
}
