using System.Collections.Generic;

public class TutorialTextRepository {

    private const string TEXT_WELCOME = "Добро пожаловать в Epic Sail.Эта демонстрация - прототип основных механик которые будут использоваться в нашей игре! Много ещё не готово, но не расстраивайся, всё впереди!";
    private const string TEXT_MOVEMENT_TUTORIAL = "Этот уровень линейный. На таких уровнях кораблик движется под воздействием течения, а ты можешь корректировать его курс, ускорять или замедлять. Попробуй свайпнуть по кораблику.";
    private const string TEXT_COIN_TUTORIAL = "Ты только что подобрал монетку. В мире Epic Sail - это ценный ресурс, за который ты сможешь улучшить кораблик, выбрать дизайн ветерка и даже купить второй шанс при столкновении с препятствием. Попробуй собрать все 16 монеток на уровне.";
    private const string TEXT_OBSTACLE_TUTORIAL = "На каждом уровне есть препятствия. Столнувшись с ним, тебе придётся начать уровень заново или потратить ценные монетки. Сейчас ты можешь двигаться дальше, но постарайся больше не сталкиваться с ними)";
    private const string TEXT_OPEN_LEVEL_TUTORIAL = "Это открытый уровень! Здесь на кораблик не действуют никакие внешние силы и ты волен двигаться куда захочешь и взаимодействовать с откружением! Пока что здесь пустовато, но поверь, уже очень скоро тебя будет ждать масса загадок и испытаний на открытых уровнях подобных этому. А пока ты можешь собрать оставшиеся монетки и насладиться свободой передвижения, либо выйти в главное меню и начать демо заново.";

    public Dictionary<string, string> TutorialsText {
        get {
            Dictionary<string, string> tutorialsText = new Dictionary<string, string>();
            tutorialsText.Add(GameManager.TAG_WELCOME_WINDOW, TEXT_WELCOME);
            tutorialsText.Add(GameManager.TAG_MOVEMENT_TUTORIAL, TEXT_MOVEMENT_TUTORIAL);
            tutorialsText.Add(GameManager.TAG_COIN_TUTORIAL, TEXT_COIN_TUTORIAL);
            tutorialsText.Add(GameManager.TAG_OBSTACLE_TUTORIAL, TEXT_OBSTACLE_TUTORIAL);
            tutorialsText.Add(GameManager.TAG_OPEN_LEVEL_TUTORIAL, TEXT_OPEN_LEVEL_TUTORIAL);

            return tutorialsText;
        }
    }
}