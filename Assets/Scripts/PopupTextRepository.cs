using System.Collections.Generic;

public class PopupTextRepository {

    private const string TEXT_WELCOME = "����� ���������� � Epic Sail.��� ������������ - �������� �������� ������� ������� ����� �������������� � ����� ����! ����� ��� �� ������, �� �� �������������, �� �������!";
    private const string TEXT_MOVEMENT_TUTORIAL = "���� ������� ��������. �� ����� ������� �������� �������� ��� ������������ �������, � �� ������ �������������� ��� ����, �������� ��� ���������. �������� ��������� �� ���������.";
    private const string TEXT_COIN_TUTORIAL = "����� �� ����� ���������� �������. � ���� Epic Sail - ��� ������ ������, �� ������� �� ������� �������� ��������, ������� ������ ������� � ���� ������ ������ ���� ��� ������������ � ������������. �������� ������� ��� 16 ������� �� ������.";
    private const string TEXT_STATIC_OBSTACLE_TUTORIAL = "�� ������ ���� ���� ������ ���� �����������. ������� ����������� ����������� - ��� ���������� � ������������ � ����� �� ���, ���� ������� ������ ������� ������ ��� ��������� ������ �������. ���������� ��������� �� ��������.";
    private const string TEXT_FLOATABLE_OBSTACLE_TUTORIAL = "��� �������� ����������. �� ������ ����������������� � ���� ����� ��� � � ����������. �������� ������ �� �� ������ ����.";
    private const string TEXT_BREAKABLE_OBSTACLE_TUTORIAL = "��� ����������� �����������. � ���� ���� ����� �����������������. �������� ��������� �� ���� ��������� ���, ����� �������. � ������� �� ����� ����������� ����� �������� ������ �����.";
    private const string TEXT_MATCHABLE_OBSTACLE_TUTORIAL = "� ������� ��������� ��� �������� �����������. �������� ��������� ����� ����� ���������� ����������� � ��������, ��� ������. � ������� �� ��� �� ������ �������� �������.";
    private const string TEXT_OPEN_LEVEL_TUTORIAL = "����� ���������� �� �������� �������!";
    private const string TEXT_TUTORIAL_LEVEL_COMPLETION = "�����������!�� ������ ��������!������ ���� �������� ����������� �����.";

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