using SpaceShooter.gui;
using SpaceShooter.resources;
using SpaceShooter.utils;

namespace SpaceShooter.src.gui.options
{
    public partial class OptionsForm : CustomForm
    {
        private const float okBtnHeightRatio = 0.05f;
        private const float okBtnWidthRatio = 0.178f;
        private const float marginRatio = 0.02f;

        private readonly Dictionary<string, List<(Bitmap, Size)>> pictureBoxOptionDetails = new Dictionary<string, List<(Bitmap, Size)>>
        {
            { "Spaceship Movement", new List<(Bitmap, Size)>() { (Resources.img_arrow_keys, new Size(80, 50)), (Resources.img_wasd_keys, new Size(80, 50)) } },
            { "Fire Laser Blaster", new List<(Bitmap, Size)>() { (Resources.img_space_key, new Size(35, 35)), (Resources.img_e_key, new Size(35, 35)) } },
            { "Pause/Resume Game", new List<(Bitmap, Size)>() { (Resources.img_esc_key, new Size(35, 35)), (Resources.img_p_key, new Size(35, 35)) } }
        };

        private readonly List<(string, Action, Action)> audioOptionDetails = new List<(string, Action, Action)>()
        {
            ("Music", AudioPlayer.Player.PlayBackgroundMusic, AudioPlayer.Player.StopBackgroundMusic),
            ("SFX", AudioPlayer.Player.ActivateOutputDevice, AudioPlayer.Player.MuteOutputDevice)
        };

        private List<PictureBoxOptionsGroup> pictureBoxOptionGroups;
        private CheckBoxOptionsGroup audioOptions;

        public OptionsForm(bool isPauseMenuInstance = false)
        {
            InitializeComponent();
            setBackgroundImage();

            if (isPauseMenuInstance)
                FormClosed -= AppManager.OnSubFormClosed;

            FormTitleLabel titleLabel = new FormTitleLabel(this, new string(' ', 5) + "OPTIONS" + new string(' ', 5));

            pictureBoxOptionGroups = new List<PictureBoxOptionsGroup>();

            foreach (var entry in pictureBoxOptionDetails)
            {
                int initY = (pictureBoxOptionGroups.Count == 0 ? titleLabel.Top + titleLabel.Height : pictureBoxOptionGroups.Last().Top + pictureBoxOptionGroups.Last().Height);
                PictureBoxOptionsGroup newPictureBoxOptionsGroup = new PictureBoxOptionsGroup(this, entry.Key, entry.Value)
                {
                    Top = initY + (int)(Height * marginRatio)
                };
                pictureBoxOptionGroups.Add(newPictureBoxOptionsGroup);
            }

            audioOptions = new CheckBoxOptionsGroup(this, "Audio", audioOptionDetails)
            {
                Top = pictureBoxOptionGroups.Last().Top + pictureBoxOptionGroups.Last().Height + (int)(Height * marginRatio),
            };

            var okBtn = new CustomButton(this, "OK", okBtnWidthRatio, okBtnHeightRatio)
            {
                Top = audioOptions.Top + audioOptions.Height + (int)(Height * marginRatio),
            };
            okBtn.Left = ClientRectangle.Width / 2 - okBtn.Width / 2;
            okBtn.Click += (sender, e) => Close();

            FormClosing += onFormClosing;
        }

        private void onFormClosing(object? sender, EventArgs e)
        {
            foreach (var pictureBoxOptionGroup in pictureBoxOptionGroups)
                pictureBoxOptionGroup.DisposePictureBoxes();

            Task.Run(() => storeOptions());
        }

        private void storeOptions()
        {
            Dictionary<string, bool> selected = audioOptions.getSelected();
            foreach (var option in selected)
                DatabaseManager.AddOrUpdateOptionEntry(option.Key, option.Value);
        }
    }
}
