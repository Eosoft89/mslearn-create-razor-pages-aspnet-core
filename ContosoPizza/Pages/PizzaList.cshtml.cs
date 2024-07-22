using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel
    {
        private readonly PizzaService service;
        public IList<Pizza> PizzaList { get; set; } = default!; //PizzaList se inicializa en default! para indicar al compilador que se inicializará más adelante, por lo que no se requieren comprobaciones de seguridad nulas.
        [BindProperty]  //El atributo BindProperty se usa para enlazar la propiedad NewPizza a la página de Razor. Al realizarse una solicitud HTTP POST, la propiedad NewPizza se rellenará con la entrada del usuario.
        public Pizza NewPizza { get; set; } = default!; //La palabra clave default! se usa para inicializar la propiedad NewPizza en null. Esto impide que el compilador genere una advertencia sobre la propiedad NewPizza que no se inicializa

        public PizzaListModel(PizzaService service) //La inyección de dependencias proporciona el objeto PizzaService
        {
            this.service = service;
        }
        
        public void OnGet()
        {
            PizzaList = service.GetPizzas();
        }
        
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewPizza == null){ // IsValid se usa para determinar si la entrada del usuario es válida.
                return Page();                          // Las reglas de validación se deducen a partir de atributos (como Required y Range) en la clase Pizza en Models\Pizza.cs.
            }

            service.AddPizza(NewPizza); 

            return RedirectToAction("Get"); //RedirectToAction se usa para redirigir al usuario al controlador de página Get, que volverá a representar la página con la lista actualizada de pizzas.
        }

        public IActionResult OnPostDelete(int id)
        {
            service.DeletePizza(id);

            return RedirectToAction("Get");
        }
    }
}
