using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace PainelComControleseMascaras
{
    public partial class Form1 : Form
    {
        private readonly Color corFundo = Color.FromArgb(244, 247, 251);
        private readonly Color corPrimaria = Color.FromArgb(15, 118, 110);
        private readonly Color corTexto = Color.FromArgb(30, 41, 59);
        private readonly Color corSecundaria = Color.FromArgb(100, 116, 139);
        private TableLayoutPanel layoutPrincipal;
        private Panel painelBoasVindas;
        private Panel painelDados;
        private TextBox txtNome;
        private MaskedTextBox mtbCep;
        private MaskedTextBox mtbTelefone;
        private MaskedTextBox mtbData;
        private ToolStripMenuItem itemAbrirPainel;
        private ToolStripMenuItem itemFecharPainel;
        private ToolStripMenuItem itemHabilitarCampos;
        private ToolStripMenuItem itemBarraStatus;
        private ToolStripStatusLabel statusLabel;

        public Form1()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CriarMenu();
            CriarConteudo();
        }

        private void ConfigurarFormulario()
        {
            Text = "Painel com Controles e Máscaras";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(940, 720);
            MinimumSize = new Size(760, 620);
            BackColor = corFundo;
            Font = new Font("Segoe UI", 10F);
            AutoScaleMode = AutoScaleMode.Font;

            layoutPrincipal = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Margin = new Padding(0),
                Padding = new Padding(0),
                BackColor = corFundo
            };
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 132F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Controls.Add(layoutPrincipal);
        }

        private void CriarMenu()
        {
            var menu = new MenuStrip
            {
                BackColor = Color.White,
                ForeColor = corTexto,
                Font = new Font("Segoe UI", 9.5F),
                Padding = new Padding(8, 5, 0, 5)
            };

            var arquivo = new ToolStripMenuItem("&Arquivo");
            var novo = new ToolStripMenuItem("&Novo cadastro", null, (s, e) => NovoCadastro())
            {
                ShortcutKeys = Keys.Control | Keys.N
            };
            var sair = new ToolStripMenuItem("Sai&r", null, (s, e) => ConfirmarSaida());
            arquivo.DropDownItems.Add(novo);
            arquivo.DropDownItems.Add(new ToolStripSeparator());
            arquivo.DropDownItems.Add(sair);

            var painel = new ToolStripMenuItem("&Painel");
            itemAbrirPainel = new ToolStripMenuItem("&Ativar painel", null, (s, e) => AbrirPainel())
            {
                ShortcutKeys = Keys.Control | Keys.P
            };
            itemFecharPainel = new ToolStripMenuItem("&Fechar painel", null, (s, e) => FecharPainel())
            {
                Enabled = false
            };
            itemHabilitarCampos = new ToolStripMenuItem("&Habilitar edição", null, (s, e) => AlternarCampos())
            {
                Checked = true,
                CheckOnClick = true,
                Enabled = false
            };
            painel.DropDownItems.Add(itemAbrirPainel);
            painel.DropDownItems.Add(itemFecharPainel);
            painel.DropDownItems.Add(new ToolStripSeparator());
            painel.DropDownItems.Add(itemHabilitarCampos);

            var editar = new ToolStripMenuItem("&Editar");
            var limpar = new ToolStripMenuItem("&Limpar campos", null, (s, e) => LimparCampos())
            {
                ShortcutKeys = Keys.Control | Keys.L
            };
            editar.DropDownItems.Add(limpar);

            var exibir = new ToolStripMenuItem("E&xibir");
            itemBarraStatus = new ToolStripMenuItem("Barra de &status", null, (s, e) => AlternarBarraStatus())
            {
                Checked = true,
                CheckOnClick = true
            };
            exibir.DropDownItems.Add(itemBarraStatus);

            var ajuda = new ToolStripMenuItem("A&juda");
            ajuda.DropDownItems.Add(new ToolStripMenuItem("&Sobre", null, (s, e) => ExibirSobre()));

            menu.Items.AddRange(new ToolStripItem[] { arquivo, painel, editar, exibir, ajuda });
            menu.Dock = DockStyle.Fill;
            MainMenuStrip = menu;
            layoutPrincipal.Controls.Add(menu, 0, 0);

            var barraStatus = new StatusStrip
            {
                BackColor = Color.White,
                ForeColor = corSecundaria,
                SizingGrip = false,
                Dock = DockStyle.Fill
            };
            statusLabel = new ToolStripStatusLabel("Pronto para iniciar") { Spring = true, TextAlign = ContentAlignment.MiddleLeft };
            barraStatus.Items.Add(statusLabel);
            layoutPrincipal.Controls.Add(barraStatus, 0, 3);
        }

        private void CriarConteudo()
        {
            var cabecalho = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = corPrimaria,
                Padding = new Padding(34, 18, 30, 16),
                Margin = new Padding(0)
            };
            var layoutCabecalho = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = corPrimaria,
                Margin = new Padding(0)
            };
            layoutCabecalho.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCabecalho.RowStyles.Add(new RowStyle(SizeType.Percent, 58F));
            layoutCabecalho.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            var titulo = new Label
            {
                Text = "Painel com Controles e Máscaras",
                Font = new Font("Segoe UI Semibold", 21F, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            var subtitulo = new Label
            {
                Text = "Gerencie seus dados pessoais com praticidade e segurança.",
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(220, 252, 245),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            layoutCabecalho.Controls.Add(titulo, 0, 0);
            layoutCabecalho.Controls.Add(subtitulo, 0, 1);
            cabecalho.Controls.Add(layoutCabecalho);
            layoutPrincipal.Controls.Add(cabecalho, 0, 1);

            var area = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(34, 26, 34, 28),
                BackColor = corFundo,
                Margin = new Padding(0)
            };
            layoutPrincipal.Controls.Add(area, 0, 2);

            painelBoasVindas = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(24),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0)
            };
            var layoutBoasVindas = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Color.White,
                Margin = new Padding(0)
            };
            layoutBoasVindas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBoasVindas.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            layoutBoasVindas.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            layoutBoasVindas.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            layoutBoasVindas.RowStyles.Add(new RowStyle(SizeType.Percent, 24F));
            var icone = new Label
            {
                Text = "✦",
                Font = new Font("Segoe UI", 32F, FontStyle.Bold),
                ForeColor = corPrimaria,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.BottomCenter
            };
            var tituloBemVindo = new Label
            {
                Text = "Seja bem-vindo!",
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = corTexto,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            var textoBemVindo = new Label
            {
                Text = "Ative o painel para preencher nome, CEP, telefone e data de nascimento.\nAs máscaras ajudam a manter cada informação no formato correto.",
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = corSecundaria,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.TopCenter
            };
            var botaoAtivar = CriarBotao("Ativar painel", corPrimaria, Color.White, 180, 44);
            botaoAtivar.Anchor = AnchorStyles.None;
            botaoAtivar.Click += (s, e) => AbrirPainel();
            layoutBoasVindas.Controls.Add(icone, 0, 0);
            layoutBoasVindas.Controls.Add(tituloBemVindo, 0, 1);
            layoutBoasVindas.Controls.Add(textoBemVindo, 0, 2);
            layoutBoasVindas.Controls.Add(botaoAtivar, 0, 3);
            painelBoasVindas.Controls.Add(layoutBoasVindas);

            painelDados = CriarPainelDados();
            painelDados.Visible = false;
            area.Controls.Add(painelDados);
            area.Controls.Add(painelBoasVindas);
        }

        private Panel CriarPainelDados()
        {
            var painel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(26),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0)
            };
            var layoutDados = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 8,
                BackColor = Color.White,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            layoutDados.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 14F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));

            var titulo = new Label
            {
                Text = "Dados pessoais",
                Font = new Font("Segoe UI Semibold", 17F, FontStyle.Bold),
                ForeColor = corTexto,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 2)
            };
            var descricao = new Label
            {
                Text = "Preencha os campos abaixo. Os formatos são aplicados automaticamente.",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = corSecundaria,
                AutoSize = true,
                Margin = new Padding(0)
            };
            layoutDados.Controls.Add(titulo, 0, 0);
            layoutDados.Controls.Add(descricao, 0, 1);

            var grade = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                BackColor = Color.White,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };
            grade.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            grade.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            grade.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            grade.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            txtNome = CriarCampoTexto();
            mtbCep = CriarCampoMascarado("00000-000");
            mtbTelefone = CriarCampoMascarado("(00) 00000-0000");
            mtbData = CriarCampoMascarado("00/00/0000");
            grade.Controls.Add(CriarGrupoCampo("Nome completo", txtNome), 0, 0);
            grade.Controls.Add(CriarGrupoCampo("CEP", mtbCep), 1, 0);
            grade.Controls.Add(CriarGrupoCampo("Telefone", mtbTelefone), 0, 1);
            grade.Controls.Add(CriarGrupoCampo("Data de nascimento", mtbData), 1, 1);
            layoutDados.Controls.Add(grade, 0, 3);

            var linha = new Panel
            {
                BackColor = Color.FromArgb(226, 232, 240),
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };
            layoutDados.Controls.Add(linha, 0, 5);

            var botoes = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.White,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            var botaoValidar = CriarBotao("Validar dados", corPrimaria, Color.White, 145, 40);
            botaoValidar.Margin = new Padding(0, 0, 12, 0);
            botaoValidar.Click += (s, e) => ValidarDados();
            var botaoLimpar = CriarBotao("Limpar", Color.FromArgb(241, 245, 249), corTexto, 110, 40);
            botaoLimpar.Margin = new Padding(0, 0, 12, 0);
            botaoLimpar.Click += (s, e) => LimparCampos();
            var botaoFechar = CriarBotao("Fechar painel", Color.FromArgb(241, 245, 249), corTexto, 145, 40);
            botaoFechar.Click += (s, e) => FecharPainel();
            botoes.Controls.Add(botaoValidar);
            botoes.Controls.Add(botaoLimpar);
            botoes.Controls.Add(botaoFechar);
            layoutDados.Controls.Add(botoes, 0, 7);
            painel.Controls.Add(layoutDados);

            ConfigurarFoco(txtNome);
            ConfigurarFoco(mtbCep);
            ConfigurarFoco(mtbTelefone);
            ConfigurarFoco(mtbData);
            ConfigurarMenuContextual(txtNome);
            ConfigurarMenuContextual(mtbCep);
            ConfigurarMenuContextual(mtbTelefone);
            ConfigurarMenuContextual(mtbData);
            return painel;
        }

        private Panel CriarGrupoCampo(string legenda, Control campo)
        {
            var grupo = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 8, 18, 8),
                BackColor = Color.White
            };
            var label = new Label
            {
                Text = legenda,
                Dock = DockStyle.Top,
                Height = 25,
                ForeColor = corTexto,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
            };
            campo.Dock = DockStyle.Top;
            campo.Height = 34;
            grupo.Controls.Add(campo);
            grupo.Controls.Add(label);
            return grupo;
        }

        private TextBox CriarCampoTexto()
        {
            return new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11F),
                ForeColor = corTexto,
                BackColor = Color.White,
                Margin = new Padding(0)
            };
        }

        private MaskedTextBox CriarCampoMascarado(string mascara)
        {
            return new MaskedTextBox
            {
                Mask = mascara,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11F),
                ForeColor = corTexto,
                BackColor = Color.White,
                PromptChar = '_',
                HidePromptOnLeave = true,
                TextMaskFormat = MaskFormat.IncludeLiterals
            };
        }

        private Button CriarBotao(string texto, Color fundo, Color frente, int largura, int altura)
        {
            return new Button
            {
                Text = texto,
                Size = new Size(largura, altura),
                BackColor = fundo,
                ForeColor = frente,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
        }

        private void ConfigurarFoco(Control campo)
        {
            campo.Enter += (s, e) => campo.BackColor = Color.FromArgb(254, 249, 195);
            campo.Leave += (s, e) => campo.BackColor = Color.White;
        }

        private void ConfigurarMenuContextual(TextBoxBase campo)
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add("Selecionar tudo", null, (s, e) => campo.SelectAll());
            menu.Items.Add("Limpar campo", null, (s, e) => campo.Clear());
            campo.ContextMenuStrip = menu;
        }

        private void AbrirPainel()
        {
            painelBoasVindas.Visible = false;
            painelDados.Visible = true;
            itemAbrirPainel.Enabled = false;
            itemFecharPainel.Enabled = true;
            itemHabilitarCampos.Enabled = true;
            statusLabel.Text = "Painel ativado";
            txtNome.Focus();
        }

        private void FecharPainel()
        {
            painelDados.Visible = false;
            painelBoasVindas.Visible = true;
            itemAbrirPainel.Enabled = true;
            itemFecharPainel.Enabled = false;
            itemHabilitarCampos.Enabled = false;
            statusLabel.Text = "Painel fechado";
        }

        private void AlternarCampos()
        {
            bool habilitado = itemHabilitarCampos.Checked;
            txtNome.Enabled = habilitado;
            mtbCep.Enabled = habilitado;
            mtbTelefone.Enabled = habilitado;
            mtbData.Enabled = habilitado;
            statusLabel.Text = habilitado ? "Edição habilitada" : "Edição bloqueada";
        }

        private void AlternarBarraStatus()
        {
            var barra = (StatusStrip)statusLabel.Owner;
            barra.Visible = itemBarraStatus.Checked;
        }

        private void LimparCampos()
        {
            if (txtNome == null)
                return;

            txtNome.Clear();
            mtbCep.Clear();
            mtbTelefone.Clear();
            mtbData.Clear();
            statusLabel.Text = "Campos limpos";
            if (painelDados.Visible && txtNome.Enabled)
                txtNome.Focus();
        }

        private void NovoCadastro()
        {
            if (PossuiDados())
            {
                var resposta = MessageBox.Show(
                    "Deseja descartar os dados atuais e iniciar um novo cadastro?",
                    "Novo cadastro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (resposta != DialogResult.Yes)
                    return;
            }

            LimparCampos();
            AbrirPainel();
        }

        private bool PossuiDados()
        {
            return !string.IsNullOrWhiteSpace(txtNome.Text) || mtbCep.Text.Trim('_', '-', ' ').Length > 0 ||
                   mtbTelefone.Text.Trim('_', '(', ')', '-', ' ').Length > 0 ||
                   mtbData.Text.Trim('_', '/').Length > 0;
        }

        private void ValidarDados()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome completo.", "Campo obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (!mtbCep.MaskCompleted || !mtbTelefone.MaskCompleted || !mtbData.MaskCompleted)
            {
                MessageBox.Show("Preencha corretamente o CEP, o telefone e a data de nascimento.", "Dados incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!mtbCep.MaskCompleted)
                    mtbCep.Focus();
                else if (!mtbTelefone.MaskCompleted)
                    mtbTelefone.Focus();
                else
                    mtbData.Focus();
                return;
            }

            DateTime data;
            if (!DateTime.TryParseExact(mtbData.Text, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.None, out data))
            {
                MessageBox.Show("Informe uma data válida no formato dia/mês/ano.", "Data inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbData.Focus();
                return;
            }

            if (data.Date > DateTime.Today)
            {
                MessageBox.Show("A data de nascimento não pode estar no futuro.", "Data inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbData.Focus();
                return;
            }

            statusLabel.Text = "Dados validados com sucesso";
            MessageBox.Show("Cadastro validado com sucesso!\n\nNome: " + txtNome.Text.Trim() +
                "\nCEP: " + mtbCep.Text + "\nTelefone: " + mtbTelefone.Text + "\nNascimento: " + mtbData.Text,
                "Cadastro válido", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ConfirmarSaida()
        {
            var resposta = MessageBox.Show("Deseja realmente sair do sistema?", "Confirmação de saída",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resposta == DialogResult.Yes)
                Close();
        }

        private void ExibirSobre()
        {
            MessageBox.Show("Painel com Controles e Máscaras\nVersão 1.0\n\nAplicação demonstrativa de menus, painéis e campos mascarados.",
                "Sobre", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
