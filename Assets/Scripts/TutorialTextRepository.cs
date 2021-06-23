using System.Collections.Generic;

public class TutorialTextRepository {

    private const string TEXT_WELCOME = "����� ���������� � Epic Sail.��� ������������ - �������� �������� ������� ������� ����� �������������� � ����� ����! ����� ��� �� ������, �� �� �������������, �� �������!";
    private const string TEXT_MOVEMENT_TUTORIAL = "���� ������� ��������. �� ����� ������� �������� �������� ��� ������������ �������, � �� ������ �������������� ��� ����, �������� ��� ���������. �������� ��������� �� ���������.";
    private const string TEXT_COIN_TUTORIAL = "�� ������ ��� �������� �������. � ���� Epic Sail - ��� ������ ������, �� ������� �� ������� �������� ��������, ������� ������ ������� � ���� ������ ������ ���� ��� ������������ � ������������. �������� ������� ��� 16 ������� �� ������.";
    private const string TEXT_OBSTACLE_TUTORIAL = "�� ������ ������ ���� �����������. ����������� � ���, ���� ������� ������ ������� ������ ��� ��������� ������ �������. ������ �� ������ ��������� ������, �� ���������� ������ �� ������������ � ����)";
    private const string TEXT_OPEN_LEVEL_TUTORIAL = "��� �������� �������! ����� �� �������� �� ��������� ������� ������� ���� � �� ����� ��������� ���� �������� � ����������������� � �����������! ���� ��� ����� ���������, �� ������, ��� ����� ����� ���� ����� ����� ����� ������� � ��������� �� �������� ������� �������� �����. � ���� �� ������ ������� ���������� ������� � ����������� �������� ������������, ���� ����� � ������� ���� � ������ ���� ������.";

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