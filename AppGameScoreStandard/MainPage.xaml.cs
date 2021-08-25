using AppGameScoreStandard.Models;
using AppGameScoreStandard.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppGameScoreStandard
{
    public partial class MainPage : ContentPage
    {
        private GameScore score;
        GameScoreApi api;

        public MainPage()
        {
            InitializeComponent();
            api = new GameScoreApi();
        }

        private void LimparCampos()
        {
            EntId.Text = "";
            EntHighScore.Text = "";
            EntGame.Text = "";
            EntName.Text = "";
            EntPhrase.Text = "";
            EntEmail.Text = "";
        }

        private async void BtLocalizar_CLicked(object sender, EventArgs e)
        {
            try
            {
                score = await api.GetHighScore(Convert.ToInt32(EntId.Text));
                if (score.Id > 0)
                {
                    EntHighScore.Text = score.Highscore.ToString();
                    EntGame.Text = score.Game;
                    EntName.Text = score.Name;
                    EntPhrase.Text = score.Phrase;
                    EntEmail.Text = score.Email;
                    BtCadastrar.Text = "Atualizar";
                }
                else
                {
                    LimparCampos();
                    BtCadastrar.Text = "Cadastrar";
                    await DisplayAlert("Aviso", "Cadastro não existente." , "OK");
                }
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }
        }

        private void BtExcluir_Clicked(object sender, EventArgs e)
        {

        }

        private void BtCadastrar_Clicked(object sender, EventArgs e)
        {

        }
    }
}
