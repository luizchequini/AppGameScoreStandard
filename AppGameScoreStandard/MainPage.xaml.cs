using AppGameScoreStandard.Models;
using AppGameScoreStandard.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;

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
                    await DisplayAlert("Aviso", "Cadastro não existente.", "OK");
                }
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }
        }

        private async void BtExcluir_Clicked(object sender, EventArgs e)
        {
            try
            {
                score = await api.GetHighScore(Convert.ToInt32(EntId.Text));
                if (score.Id > 0)
                {
                    bool action = await DisplayAlert("Alerta", "Deseja realmente excluir este registro?", "Sim", "Não");

                    if (action)
                    {
                        LimparCampos();
                        BtCadastrar.Text = "Cadastrar";
                        await api.DeleteHighScore(score.Id);
                        await this.DisplayToastAsync("Registro excluído com sucesso!", 5000);
                    }
                }
                else
                {
                    LimparCampos();
                    BtCadastrar.Text = "Cadastrar";
                    await DisplayAlert("Aviso", "Cadastro não existente.", "OK");
                }
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }
        }

        private async void BtCadastrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                score = new GameScore
                {
                    Highscore = Convert.ToInt32(EntHighScore.Text),
                    Game = EntGame.Text,
                    Name = EntName.Text,
                    Phrase = EntPhrase.Text,
                    Email = EntEmail.Text
                };

                if (BtCadastrar.Text.Equals("Atualizar"))
                {
                    score.Id = Convert.ToInt32(EntId);
                    await api.UpdateHighScore(score);
                }
                else
                {
                    await api.CreateHighScore(score);
                }

                LimparCampos();
                await DisplayAlert("Sucesso", "Operação realizada com sucesso!", "Ok");
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }
        }
    }
}
