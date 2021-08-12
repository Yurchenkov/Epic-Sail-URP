using System.Collections.Generic;

public class PopupTextRepository {

    private const string TEXT_WELCOME = "ƒобро пожаловать в Epic Sail.Ёта демонстраци€ - прототип основных механик которые будут использоватьс€ в нашей игре! ћного ещЄ не готово, но не расстраивайс€, всЄ впереди!";
    private const string TEXT_MOVEMENT_TUTORIAL = "Ётот уровень линейный. Ќа таких уровн€х кораблик движетс€ под воздействием течени€, а ты можешь корректировать его курс, ускор€ть или замедл€ть. ѕопробуй свайпнуть по кораблику.";
    private const string TEXT_COIN_TUTORIAL = "ѕр€мо по курсу разбросаны монетки. ¬ мире Epic Sail - это ценный ресурс, за который ты сможешь улучшить кораблик, выбрать дизайн ветерка и даже купить второй шанс при столкновении с преп€тствием. ѕопробуй собрать все 16 монеток на уровне.";
    private const string TEXT_OBSTACLE_TUTORIAL = "Ќа каждом уровне есть преп€тстви€. —толнувшись с ним, тебе придЄтс€ начать уровень заново или потратить ценные монетки. —ейчас ты можешь двигатьс€ дальше, но постарайс€ больше не сталкиватьс€ с ними)";
    private const string TEXT_OPEN_LEVEL_TUTORIAL = "Ёто открытый уровень! «десь на кораблик не действуют никакие внешние силы и ты волен двигатьс€ куда захочешь и взаимодействовать с откружением! ѕока что здесь пустовато, но поверь, уже очень скоро теб€ будет ждать масса загадок и испытаний на открытых уровн€х подобных этому. ј пока ты можешь собрать оставшиес€ монетки и насладитьс€ свободой передвижени€, либо выйти в главное меню и попробовать бесконечный режим.";
    private const string TEXT_TUTORIAL_LEVEL_COMPLETION = "ѕоздравл€ем!“ы прошЄл обучение!“еперь ты можешь играть в бесконечном режиме.";

    public Dictionary<string, string> PopupsText {
        get {
            Dictionary<string, string> popupsText = new Dictionary<string, string>();
            popupsText.Add(Constants.TAG_WELCOME_WINDOW, TEXT_WELCOME);
            popupsText.Add(Constants.TAG_MOVEMENT_TUTORIAL, TEXT_MOVEMENT_TUTORIAL);
            popupsText.Add(Constants.TAG_COIN_TUTORIAL, TEXT_COIN_TUTORIAL);
            popupsText.Add(Constants.TAG_OBSTACLE_TUTORIAL, TEXT_OBSTACLE_TUTORIAL);
            popupsText.Add(Constants.TAG_OPEN_LEVEL_TUTORIAL, TEXT_OPEN_LEVEL_TUTORIAL);
            popupsText.Add(Constants.TAG_TUTORIAL_LEVEL_COMPLETION, TEXT_TUTORIAL_LEVEL_COMPLETION);

            return popupsText;
        }
    }
}