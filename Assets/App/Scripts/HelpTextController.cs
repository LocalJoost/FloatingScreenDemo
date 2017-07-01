using HoloToolkitExtensions.Animation;
using HoloToolkitExtensions.Messaging;

public class HelpTextController : BaseTextScreenController
{
    public override void Start()
    {
        base.Start();
        Messenger.Instance.AddListener<ShowHelpMessage>(ShowHelp);
    }

    private void ShowHelp(ShowHelpMessage arg1)
    {
        Show();
    }
}
