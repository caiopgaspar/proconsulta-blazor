using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProConsulta.Models;
using ProConsulta.Repositories.Medicos;

namespace ProConsulta.Components.Pages.Medicos
{
    public class IndexMedicoPage : ComponentBase
    {
        [Inject]
        public IMedicoRepository Repository { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [Inject]
        public NavigationManager Navigation { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        public List<Medico> Medicos { get; set; } = new(); /*List<Medico>();*/

        public async Task DeleteMedicoAsync(Medico medico)
        {
            try
            {
                var result = await DialogService.ShowMessageBox
                (
                    "Atenção",
                    $"Deseja excluir o médico {medico.Nome}?",
                    yesText: "SIM",
                    noText: "NÃO"
                );

                if(result is true)
                {
                    await Repository.DeleteByIdAsync(medico.Id);
                    Snackbar.Add("Médico excluído com sucesso!", Severity.Success);
                    await OnInitializedAsync();
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public void GoToUpdate(int id)
            => Navigation.NavigateTo($"/medicos/update/{id}");

        protected override async Task OnInitializedAsync()
        {
            Medicos = await Repository.GetAllAsync();
        }
    }
}
