using System.Collections.Generic;

public class PopupTextRepository {

    private const string TEXT_WELCOME = "����� ���������� � Epic Sail.��� ������������ - �������� �������� ������� ������� ����� �������������� � ����� ����! ����� ��� �� ������, �� �� �������������, �� �������!";
    private const string TEXT_MOVEMENT_TUTORIAL = "���� ������� ��������. �� ����� ������� �������� �������� ��� ������������ �������, � �� ������ �������������� ��� ����, �������� ��� ���������. �������� ��������� �� ���������.";
    private const string TEXT_COIN_TUTORIAL = "����� �� ����� ���������� �������. � ���� Epic Sail - ��� ������ ������, �� ������� �� ������� �������� ��������, ������� ������ ������� � ���� ������ ������ ���� ��� ������������ � ������������. �������� ������� ��� 16 ������� �� ������.";
    private const string TEXT_OBSTACLE_TUTORIAL = "�� ������ ������ ���� �����������. ����������� � ���, ���� ������� ������ ������� ������ ��� ��������� ������ �������. ������ �� ������ ��������� ������, �� ���������� ������ �� ������������ � ����)";
    private const string TEXT_OPEN_LEVEL_TUTORIAL = "��� �������� �������! ����� �� �������� �� ��������� ������� ������� ���� � �� ����� ��������� ���� �������� � ����������������� � �����������! ���� ��� ����� ���������, �� ������, ��� ����� ����� ���� ����� ����� ����� ������� � ��������� �� �������� ������� �������� �����. � ���� �� ������ ������� ���������� ������� � ����������� �������� ������������, ���� ����� � ������� ���� � ����������� ����������� �����.";

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