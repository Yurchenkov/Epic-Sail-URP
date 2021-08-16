using System.Collections.Generic;

public class PopupTextRepository {

    private const string TEXT_WELCOME = "Добро пожаловать в Epic Sail.Эта демонстрация - прототип основных механик которые будут использоваться в нашей игре! Много ещё не готово, но не расстраивайся, всё впереди!";
    private const string TEXT_MOVEMENT_TUTORIAL = "Этот уровень линейный. На таких уровнях кораблик движется под воздействием течения, а ты можешь корректировать его курс, ускорять или замедлять. Попробуй свайпнуть по кораблику.";
    private const string TEXT_COIN_TUTORIAL = "Прямо по курсу разбросаны монетки. В мире Epic Sail - это ценный ресурс, за который ты сможешь улучшить кораблик, выбрать дизайн ветерка и даже купить второй шанс при столкновении с препятствием. Попробуй собрать все 16 монеток на уровне.";
    private const string TEXT_STATIC_OBSTACLE_TUTORIAL = "На уровне есть есть разные виды препятствий. Впереди статические препятствия - они неподвижны и столкнувшись с одним из них, тебе придётся начать уровень заново или потратить ценные монетки. Постарайся обплывать их стороной.";
    private const string TEXT_FLOATABLE_OBSTACLE_TUTORIAL = "Это плавучие препятсвия. Ты можешь взаимодействовать с ними также как и с корабликом. Попробуй убрать их со своего пути.";
    private const string TEXT_BREAKABLE_OBSTACLE_TUTORIAL = "Это разрушаемые препятствия. С ними тоже можно взаимодействовать. Попробуй свайпнуть по нему несколько раз, чтобы сломать. В будущем из таких препятствий будут выпадать ценные призы.";
    private const string TEXT_MATCHABLE_OBSTACLE_TUTORIAL = "И наконец последний вид плавучих препятствий. Попробуй столкнуть между собой одинаковые препятствия и посмотри, что выйдет. В будущем за это ты будешь получать награды.";
    private const string TEXT_OPEN_LEVEL_TUTORIAL = "Добро пожаловать на открытый уровень!";
    private const string TEXT_TUTORIAL_LEVEL_COMPLETION = "Поздравляем!Ты прошёл обучение!Теперь тебе доступен бесконечный режим.";

    public Dictionary<string, string> PopupsText {
        get {
            Dictionary<string, string> popupsText = new Dictionary<string, string>();
            popupsText.Add(Constants.TAG_WELCOME_WINDOW, TEXT_WELCOME);
            popupsText.Add(Constants.TAG_MOVEMENT_TUTORIAL, TEXT_MOVEMENT_TUTORIAL);
            popupsText.Add(Constants.TAG_COIN_TUTORIAL, TEXT_COIN_TUTORIAL);
            popupsText.Add(Constants.TAG_STATIC_OBSTACLE_TUTORIAL, TEXT_STATIC_OBSTACLE_TUTORIAL);
            popupsText.Add(Constants.TAG_FLOATABLE_OBSTACLE_TUTORIAL, TEXT_FLOATABLE_OBSTACLE_TUTORIAL);
            popupsText.Add(Constants.TAG_BREAKABLE_OBSTACLE_TUTORIAL, TEXT_BREAKABLE_OBSTACLE_TUTORIAL);
            popupsText.Add(Constants.TAG_MATCHABLE_OBSTACLE_TUTORIAL, TEXT_MATCHABLE_OBSTACLE_TUTORIAL);
            popupsText.Add(Constants.TAG_OPEN_LEVEL_TUTORIAL, TEXT_OPEN_LEVEL_TUTORIAL);
            popupsText.Add(Constants.TAG_TUTORIAL_LEVEL_COMPLETION, TEXT_TUTORIAL_LEVEL_COMPLETION);

            return popupsText;
        }
    }
}