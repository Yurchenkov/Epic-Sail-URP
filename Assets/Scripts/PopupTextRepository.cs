using System.Collections.Generic;

public class PopupTextRepository {

    private const string TEXT_WELCOME = "Добро пожаловать в Epic Sail.Эта демонстрация - прототип основных механик которые будут использоваться в нашей игре! Много ещё не готово, но не расстраивайся, всё впереди!";
    private const string TEXT_MOVEMENT_TUTORIAL = "Этот уровень линейный. На таких уровнях кораблик движется под воздействием течения, а ты можешь корректировать его курс, ускорять или замедлять. Попробуй свайпнуть по кораблику.";
    private const string TEXT_COIN_TUTORIAL = "Прямо по курсу разбросаны монетки. В мире Epic Sail - это ценный ресурс, за который ты сможешь улучшить кораблик, выбрать дизайн ветерка и даже купить второй шанс при столкновении с препятствием. Попробуй собрать все 16 монеток на уровне.";
    private const string TEXT_OBSTACLE_TUTORIAL = "На каждом уровне есть препятствия. Столнувшись с ним, тебе придётся начать уровень заново или потратить ценные монетки. Сейчас ты можешь двигаться дальше, но постарайся больше не сталкиваться с ними)";
    private const string TEXT_OPEN_LEVEL_TUTORIAL = "Это открытый уровень! Здесь на кораблик не действуют никакие внешние силы и ты волен двигаться куда захочешь и взаимодействовать с откружением! Пока что здесь пустовато, но поверь, уже очень скоро тебя будет ждать масса загадок и испытаний на открытых уровнях подобных этому. А пока ты можешь собрать оставшиеся монетки и насладиться свободой передвижения, либо выйти в главное меню и попробовать бесконечный режим.";

    public Dictionary<string, string> PopupsText {
        get {
            Dictionary<string, string> popupsText = new Dictionary<string, string>();
            popupsText.Add(Constants.TAG_WELCOME_WINDOW, TEXT_WELCOME);
            popupsText.Add(Constants.TAG_MOVEMENT_TUTORIAL, TEXT_MOVEMENT_TUTORIAL);
            popupsText.Add(Constants.TAG_COIN_TUTORIAL, TEXT_COIN_TUTORIAL);
            popupsText.Add(Constants.TAG_OBSTACLE_TUTORIAL, TEXT_OBSTACLE_TUTORIAL);
            popupsText.Add(Constants.TAG_OPEN_LEVEL_TUTORIAL, TEXT_OPEN_LEVEL_TUTORIAL);

            return popupsText;
        }
    }
}