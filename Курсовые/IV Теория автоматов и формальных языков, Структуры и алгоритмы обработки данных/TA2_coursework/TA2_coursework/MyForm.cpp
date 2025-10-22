#include "MyForm.h"
using namespace System::IO;
using namespace System;
using namespace System::Windows::Forms;
[STAThreadAttribute]
int main(array<String^>^ args)
{
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false);
	TA2coursework::MyForm form;
	Application::Run(% form);
}
bool TA2coursework::MyForm::search_id(String^* word, int* row)
{
	for (size_t i = 0; i <= *row; i++)
		if (Convert::ToString(table_id->Rows[i]->Cells[1]->Value) == *word)
			return true;
	return false;
}
void TA2coursework::MyForm::add_id(String^* word, int* row_id, int* row_keyword)
{
	if (!search_id(word, row_id) && !search_keyword(word, row_keyword)) {
		table_id->Rows[*row_id - 1]->Cells[0]->Value = Convert::ToString(*row_id);
		table_id->Rows[*row_id - 1]->Cells[1]->Value = *word;
		table_id->Rows[*row_id - 1]->Cells[2]->Value = "id" + Convert::ToString(*row_id - 1);
		table_id->RowCount = ++ * row_id + 1;
	}
	*word = "";
}
bool TA2coursework::MyForm::search_keyword(String^* word, int* row)
{
	for (size_t i = 0; i <= *row; i++)
		if (Convert::ToString(table_lexeme->Rows[i]->Cells[1]->Value) == *word)
			return true;
	return false;
}
void TA2coursework::MyForm::add_keyword(String^* word, int* row)
{
	if (!search_keyword(word, row)) {
		table_lexeme->Rows[*row - 1]->Cells[0]->Value = Convert::ToString(*row);
		table_lexeme->Rows[*row - 1]->Cells[1]->Value = *word;
		table_lexeme->Rows[*row - 1]->Cells[2]->Value = *word;
		table_lexeme->RowCount = ++ * row + 1;
	}
	*word = "";
}
bool TA2coursework::MyForm::search_const(String^* word, int* row)
{
	for (size_t i = 0; i <= *row; i++)
		if (Convert::ToString(table_const->Rows[i]->Cells[1]->Value) == *word)
			return true;
	return false;
}
void TA2coursework::MyForm::add_const(String^* word, int* row)
{
	if (!search_const(word, row)) {
		table_const->Rows[*row - 1]->Cells[0]->Value = Convert::ToString(*row);
		table_const->Rows[*row - 1]->Cells[1]->Value = *word;
		table_const->Rows[*row - 1]->Cells[2]->Value = "const" + Convert::ToString(*row - 1);
		table_const->RowCount = ++ * row + 1;
	}
	*word = "";
}
bool TA2coursework::MyForm::search_comparison_operators(String^* word, int* row)
{
	for (size_t i = 0; i <= *row; i++)
		if (Convert::ToString(table_comparison_operators->Rows[i]->Cells[1]->Value) == *word)
			return true;
	return false;
}
void TA2coursework::MyForm::add_comparison_operators(String^* word, int* row)
{
	if (!search_comparison_operators(word, row)) {
		table_comparison_operators->Rows[*row - 1]->Cells[0]->Value = Convert::ToString(*row);
		table_comparison_operators->Rows[*row - 1]->Cells[1]->Value = *word;
		table_comparison_operators->Rows[*row - 1]->Cells[2]->Value = *word;
		table_comparison_operators->RowCount = ++ * row + 1;
	}
	*word = "";
}
bool TA2coursework::MyForm::search_operation_sings(String^* word, int* row)
{
	for (size_t i = 0; i <= *row; i++)
		if (Convert::ToString(table_operation_sings->Rows[i]->Cells[1]->Value) == *word)
			return true;
	return false;
}
void TA2coursework::MyForm::add_operation_sings(String^* word, int* row)
{
	if (!search_operation_sings(word, row)) {
		table_operation_sings->Rows[*row - 1]->Cells[0]->Value = Convert::ToString(*row);
		table_operation_sings->Rows[*row - 1]->Cells[1]->Value = *word;
		table_operation_sings->Rows[*row - 1]->Cells[2]->Value = *word;
		table_operation_sings->RowCount = ++ * row + 1;
	}
	*word = "";
}
bool TA2coursework::MyForm::search_separators(String^* word, int* row)
{
	for (size_t i = 0; i <= *row; i++)
		if (Convert::ToString(table_separators->Rows[i]->Cells[1]->Value) == *word)
			return true;
	return false;
}
void TA2coursework::MyForm::add_separators(String^* word, int* row)
{
	if (!search_separators(word, row)) {
		table_separators->Rows[*row - 1]->Cells[0]->Value = Convert::ToString(*row);
		table_separators->Rows[*row - 1]->Cells[1]->Value = *word;
		table_separators->Rows[*row - 1]->Cells[2]->Value = *word;
		table_separators->RowCount = ++ * row + 1;
	}
	*word = "";
}

bool TA2coursework::MyForm::skip(wchar_t* c)
{
	return (*c == ' ' || *c == '\n' || *c == '\r') ? true : false;
}
bool TA2coursework::MyForm::comparison_operators(wchar_t* c)
{
	return (*c == '<' || *c == '>' || *c == '=' || *c == '!') ? true : false;
}
bool TA2coursework::MyForm::operation_sings(wchar_t* c)
{
	return (*c == '=' || *c == '-' || *c == '+' || *c == '*' || *c == '/'
		|| *c == '%' || *c == '|' || *c == '&') ? true : false;
}
bool TA2coursework::MyForm::separators(wchar_t* c)
{
	return (*c == ';' || *c == '(' || *c == ')' || *c == '{' || *c == '}' || *c == ':'
		|| *c == '#' || *c == ',' || *c == '[' || *c == ']') ? true : false;
}
bool TA2coursework::MyForm::other(wchar_t* c)
{
	return (*c == '.') ? true : false;
}
bool TA2coursework::MyForm::end_condition(wchar_t* c)
{
	return (other(c) || separators(c) || comparison_operators(c) || operation_sings(c) || skip(c)) ? true : false;
}
bool TA2coursework::MyForm::allowed_character(wchar_t* c)
{
	return (*c == '_' || *c == 'a' || *c == 'b' || *c == 'c' || *c == 'd' || *c == 'e' || *c == 'f'
		|| *c == 'g' || *c == 'h' || *c == 'i' || *c == 'j' || *c == 'k' || *c == 'l' || *c == 'm'
		|| *c == 'n' || *c == 'o' || *c == 'p' || *c == 'q' || *c == 'r' || *c == 's' || *c == 't'
		|| *c == 'u' || *c == 'v' || *c == 'w' || *c == 'x' || *c == 'y' || *c == 'z' || *c == 'A'
		|| *c == 'B' || *c == 'C' || *c == 'D' || *c == 'E' || *c == 'F' || *c == 'G' || *c == 'H'
		|| *c == 'I' || *c == 'J' || *c == 'K' || *c == 'L' || *c == 'M' || *c == 'N' || *c == 'O'
		|| *c == 'P' || *c == 'Q' || *c == 'R' || *c == 'S' || *c == 'T' || *c == 'U' || *c == 'V'
		|| *c == 'W' || *c == 'X' || *c == 'Y' || *c == 'Z') ? true : false;
}
bool TA2coursework::MyForm::numbers(wchar_t* c)
{
	return (*c == '1' || *c == '2' || *c == '3' || *c == '4' || *c == '5'
		|| *c == '6' || *c == '7' || *c == '8' || *c == '9') ? true : false;
}
void TA2coursework::MyForm::error(String^* word, String^ type, int* line)
{
	if (type == "const") type = "константы";
	else if (type == "id") type = "идентефикатора";
	else if (type == "multi-line_comment") {
		type = "многострочного комментария";
		*word = "Не найден закрывающий */";
	}
	list_of_erros->Text += "Строка " + (*line + 1) + ": " + *word + " – ошибка " + type + "\n";
	*word = "";
}

void TA2coursework::MyForm::cleaning(String^* str)
{
	int S = 0, n = 0, line = 0;
	StreamWriter^ fsave = gcnew StreamWriter("buffer.txt", false);
	StreamWriter^ ftext = gcnew StreamWriter("text_out.txt", false);
	ftext->Write("      " + System::Convert::ToString(++n) + "  ");
	String^ a = "";
	for (int i = 0; i < (*str)->Length; i++)
	{
		a = System::Convert::ToString((*str)[i]);
		switch (S) {
			// состояние пустой строки
		case 0:
			if (a == " " || a == "\t" || a == "\n" || a == "\r") S = 0;
			else if (a == "/") S = 9;
			else S = 1;
			break;
			// состояние записи итогового кода
		case 1:
			if (a == " " || a == "\t") S = 2;
			else if (a == "/") S = 3;
			else if (a == "\n" || a == "\r") S = 8;
			break;
			// состояние нахождения пробелов
		case 2:
			if (a == " " || a == "\t") S = 14;
			else if (a == "/") S = 3;
			else if (a == "\n" || a == "\r") S = 8;
			else S = 1;
			break;
			// определение типа комментария
		case 3:
			if (a == "/") S = 4;
			else if (a == "*") S = 5;
			else if (a == "\n" || a == "\r") S = 8;
			else {
				a = "/" + a;
				S = 1;
			}
			break;
			// чтение однострочного комментария
		case 4:
			if (a == "\n" || a == "\r") S = 8;
			break;
			// чтение многострочного комментария
		case 5:
			if (a == "*") S = 6;
			break;
		case 6:
			if (a == "/") S = 7;
			else S = 5;
			break;
		case 7:
			if (a == "*") S = 6;
			else if (a == "\t" || a == " ") S = 2;
			else if (a == "/") S = 3;
			else if (a == "\n" || a == "\r") S = 0;
			else S = 1;
			break;
			// состояние переноса строки
		case 8:
			S = 0;
			break;
			// комментарий в начале строки
		case 9:
			if (a == "/") S = 10;
			else if (a == "*") S = 11;
			else if (a == "\n" || a == "\r") S = 0;
			else {
				a = "/" + a;
				S = 1;
			}
			break;
		case 10:
			if (a == "\n" || a == "\r") S = 0;
			break;
		case 11:
			if (a == "*") S = 12;
			break;
		case 12:
			if (a == "/") S = 13;
			else S = 11;
			break;
		case 13:
			if (a == " " || a == "\t" || a == "\n" || a == "\r") S = 0;
			else if (a == "*") S = 12;
			else if (a == "/") S = 9;
			else S = 1;
			break;
			// игнорирование пробелов
		case 14:
			if (a == "\n" || a == "\r") S = 8;
			else if (a == "/") S = 3;
			else if (a == " " || a == "\t") S = 14;
			else S = 1;
			break;
		default: MessageBox::Show(L"Ошибка при удалении пустот!", L"Программа остановлена", MessageBoxButtons::OK, MessageBoxIcon::Error); return;
		}

		// заполнение таблицы
		if (S == 8) {
			if (n < 9) ftext->Write("\n      " + ++n + "  ");
			else if (n < 99) ftext->Write("\n    " + ++n + "  ");
			else if (n < 999) ftext->Write("\n  " + ++n + "  ");
			else ftext->Write("\n" + ++n + "  ");
			fsave->Write("\n");
			line++;
			i--;
		}
		// запись символа
		else if (S == 1) ftext->Write(a), fsave->Write(a);
		else if (S == 2) ftext->Write(" "), fsave->Write(" ");
	}
	if (S == 11 || S == 5)
		error(&a, "multi-line_comment", &line);

	fsave->Write("  ");
	ftext->Close();
	fsave->Close();
	StreamReader^ fout = gcnew StreamReader("text_out.txt");
	text_out->Text = fout->ReadToEnd();
	fout->Close();
}
void TA2coursework::MyForm::processing(int* row_keyword, int* row_id, int* row_const, int* row_comparison_operators, int* row_operation_sings, int* row_separators)
{
	int S = 0, last_s, line = 0;
	StreamReader^ fout = gcnew StreamReader("buffer.txt");
	String^ str = fout->ReadToEnd();
	fout->Close();
	String^ word = "";
	wchar_t c;
	if (str != "") {
		for (int i = 0; i < str->Length; i++) {
			last_s = S;
			c = str[i];
			switch (S) {
				//0 состояние, запись идентификаторов
			case 0:
				if (skip(&c)) S = 0;
				else if (c == '"') S = 4;//строка
				else if (c == 'a') S = 8;//auto, and
				else if (c == 'l') S = 11;//long
				else if (c == 'b') S = 14;//bool, break
				else if (c == 'u') S = 21;//using, union, unsigned
				else if (c == 'c') S = 22;//case, catch, char, class, const, continue, cin, cout
				else if (c == 'x') S = 28;//xor
				else if (c == 'm') S = 31;//mutable
				else if (c == 'd') S = 44;//default, delete, do, double
				else if (c == 'e') S = 49;//else, enum, export, endl
				else if (c == 'f') S = 54;//false, float, for, friend
				else if (c == 'i') S = 58;//int, if, inline, include
				else if (c == 'n') S = 62;//namespace, new, not, nullptr
				else if (c == 'o') S = 71;//operator, or
				else if (c == 'p') S = 76;//private, protected, public
				else if (c == 's') S = 91;//short, sizeof, static, switch, string, struct, size_t, std
				else if (c == 'v') S = 103;//virtual, void, volatile
				else if (c == 'r') S = 109;//return
				else if (c == 't') S = 114;//template, this, throw, true, try, typename, typeid
				else if (c == 'w') S = 162;//wchar_t
				else if (numbers(&c)) S = 128;//константа
				else if (c == '0') S = 132;//0-константа
				else if (c == '.' || c == ',' || c == '{' || c == '}' || c == '['
					|| c == ']' || c == '(' || c == ')' || c == ';' || c == '#') S = 135;// . , { } [ ] ( ) : ; #
				else if (c == ':') S = 136;//::
				else if (c == '?') S = 136;//?
				else if (c == '=') S = 141;//==
				else if (c == '<') S = 142;//< <= <<
				else if (c == '>') S = 143;//> >= >>
				else if (c == '+') S = 144;//+ ++ +=
				else if (c == '-') S = 145;//- -- -=
				else if (c == '*') S = 146;//* *=
				else if (c == '/') S = 147;// / /=
				else if (c == '%') S = 148;// % %=
				else if (c == '|') S = 149;// | ||
				else if (c == '&') S = 150;// & &&
				else if (c == '!') S = 154;// ! != 
				else if (allowed_character(&c)) S = 1;//любой другой разрешённый символ
				else S = 155;//ошибочный символ
				break;
				//чтение идентификаторов (прочее)
			case 1:
				if (numbers(&c) || c == '0') S = 1;
				else if (allowed_character(&c)) S = 1;
				else if (end_condition(&c)) S = 0;
				else S = 155;
				break;
				//запись лексем
			case 2:
				S = 0;
				break;
				//проверка лексемы на пробел
			case 3:
				if (end_condition(&c)) S = 2;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
				//начало строки
			case 4:
				if (c == '"') S = 0;
				else S = 7;
				break;
				//запись констант
			case 5:
				S = 0;
				break;
				//ошибочное состояние для констант
			case 6:
				S = 0;
				break;
				//чтение строки
			case 7:
				if (c == '"') S = 153;
				break;
				//чтение лексем
			case 8:
				if (c == 'u') S = 9;//auto
				else if (c == 'n') S = 12;//and, friend
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 9:
				if (c == 't') S = 10;//auto
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 10:
				if (c == 'o') S = 3;//найдено auto, do
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 11:
				if (c == 'o') S = 13;//long
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 12:
				if (c == 'd') S = 3;//найдено and, friend, protected, void, unsigned, typeid
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 13:
				if (c == 'n') S = 17;//long, using
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 14:
				if (c == 'o') S = 15;//bool
				else if (c == 'r') S = 18;//break
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 15:
				if (c == 'o') S = 16;//bool
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 16:
				if (c == 'l') S = 3;//найдено bool, virtual, endl
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 17:
				if (c == 'g') S = 3;//найдено long, using, string
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 18:
				if (c == 'e') S = 19;//break
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 19:
				if (c == 'a') S = 20;//break
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 20:
				if (c == 'k') S = 3;//найдено break
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 21:
				if (c == 's') S = 25;//using
				else if (c == 'n') S = 169;//union, unsigned
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 22:
				if (c == 'a') S = 23;//case, catch
				else if (c == 'h') S = 29;//char
				else if (c == 'l') S = 32;//class
				else if (c == 'o') S = 36;//const, continue, cout
				else if (c == 'i') S = 113;//cin
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 23:
				if (c == 's') S = 24;//case, else
				else if (c == 't') S = 26;//catch
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 24:
				if (c == 'e') S = 3;//найдено case, else, mutable, false, inline, include, continue, namespace, volatile
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 25:
				if (c == 'i') S = 13;//using
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 26:
				if (c == 'c') S = 27;//catch
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 27:
				if (c == 'h') S = 3;//найдено catch, switch
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 28:
				if (c == 'o') S = 30;//xor, operator
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 29:
				if (c == 'a') S = 30;//char
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 30:
				if (c == 'r') S = 3;//найдено char, xor, for, operator, nullptr
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 31:
				if (c == 'u') S = 35;//mutable
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 32:
				if (c == 'a') S = 33;//class
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 33:
				if (c == 's') S = 43;//class
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 34:
				if (c == 's') S = 3;//найдено this
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 35:
				if (c == 't') S = 39;//mutable
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 36:
				if (c == 'n') S = 37;//const, continue
				else if (c == 'u') S = 38;//cout
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 37:
				if (c == 's') S = 38;//const
				else if (c == 't') S = 40;//continue
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 38:
				if (c == 't') S = 3;//найдено const, default, float, not, int, short, export, size_t, cout, wchar_t
				else if (c == 'l') S = 60;//inline
				else if (c == 'c') S = 157;//include
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 39:
				if (c == 'a') S = 52;//mutable
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 40:
				if (c == 'i') S = 41;//continue
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 41:
				if (c == 'n') S = 42;//continue
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 42:
				if (c == 'u') S = 24;//continue
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 43:
				if (c == 's') S = 3;//найдено class
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 44:
				if (c == 'e') S = 45;//default, delete
				else if (c == 'o') S = 160;//do, double
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 45:
				if (c == 'f') S = 46;//default
				else if (c == 'l') S = 81;//delete
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 46:
				if (c == 'a') S = 47;//default
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 47:
				if (c == 'u') S = 48;//default
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 48:
				if (c == 'l') S = 38;//default
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 49:
				if (c == 'l') S = 23;//else
				else if (c == 'n') S = 50;//enum, endl
				else if (c == 'x') S = 93;//export
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 50:
				if (c == 'u') S = 51;//enum
				else if (c == 'd') S = 16;//endl
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 51:
				if (c == 'm') S = 3;//найдено enum
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 52:
				if (c == 'b') S = 53;//mutable
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 53:
				if (c == 'l') S = 24;//mutable
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 54:
				if (c == 'a') S = 55;//false
				else if (c == 'l') S = 56;//float
				else if (c == 'o') S = 30;//for
				else if (c == 'r') S = 87;//friend
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 55:
				if (c == 'l') S = 23;//false
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 56:
				if (c == 'o') S = 57;//float
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 57:
				if (c == 'a') S = 38;//float
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 58:
				if (c == 'n') S = 38;//int, inline, include
				else if (c == 'f') S = 3;//if, sizeof
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 59:
				if (c == '-' || c == '+') S = 69;//экспонента
				else S = 130;
				break;
			case 60:
				if (c == 'i') S = 61;//inline
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 61:
				if (c == 'n') S = 24;//inline
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 62:
				if (c == 'a') S = 63;//namespace
				else if (c == 'e') S = 70;//new
				else if (c == 'o') S = 38;//not
				else if (c == 'u') S = 177;//nullptr
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 63:
				if (c == 'm') S = 64;//namespace
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 64:
				if (c == 'e') S = 65;//namespace
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 65:
				if (c == 's') S = 66;//namespace
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 66:
				if (c == 'p') S = 67;//namespace
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 67:
				if (c == 'a') S = 68;//namespace
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 68:
				if (c == 'c') S = 24;//namespace
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 69:
				if (!end_condition(&c)) S = 184;//вводится экспонента
				else S = 130;
				break;
			case 70:
				if (c == 'w') S = 3;//найдено new
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 71:
				if (c == 'p') S = 72;//operator
				else if (c == 'r') S = 3;//найдено or
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 72:
				if (c == 'e') S = 73;//operator
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 73:
				if (c == 'r') S = 74;//operator
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 74:
				if (c == 'a') S = 75;//operator
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 75:
				if (c == 't') S = 28;//operator
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 76:
				if (c == 'r') S = 77;//private, protected
				else if (c == 'u') S = 83;//public
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 77:
				if (c == 'i') S = 78;//private
				else if (c == 'o') S = 89;//protected
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 78:
				if (c == 'v') S = 79;//private
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 79:
				if (c == 'a') S = 80;//private
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 80:
				if (c == 't') S = 82;//private, delete
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 81:
				if (c == 'e') S = 80;//delete
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 82:
				if (c == 'e') S = 3;//найдено delete, private, template, typename
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 83:
				if (c == 'b') S = 84;//public
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 84:
				if (c == 'l') S = 85;//public
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 85:
				if (c == 'i') S = 86;//public
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 86:
				if (c == 'c') S = 3;//найдено public, static
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 87:
				if (c == 'i') S = 88;//friend
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 88:
				if (c == 'e') S = 8;//friend
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 89:
				if (c == 't') S = 90;//protected
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 90:
				if (c == 'e') S = 166;//protected
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 91:
				if (c == 'h') S = 92;//short
				else if (c == 'w') S = 95;//switch
				else if (c == 'i') S = 96;//sizeof, size_t
				else if (c == 't') S = 99;//static, string, struct, std
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 92:
				if (c == 'o') S = 94;//short
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 93:
				if (c == 'p') S = 92;//export
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 94:
				if (c == 'r') S = 38;//export, short
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 95:
				if (c == 'i') S = 23;//switch
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 96:
				if (c == 'z') S = 97;//sizeof, size_t
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 97:
				if (c == 'e') S = 98;//sizeof, size_t
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 98:
				if (c == 'o') S = 58;//sizeof
				else if (c == '_') S = 38;//size_t, wchar_t
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 99:
				if (c == 'a') S = 100;//static
				else if (c == 'r') S = 101;//string, struct
				else if (c == 'd') S = 3;//std
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 100:
				if (c == 't') S = 85;//static
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 101:
				if (c == 'i') S = 13;//string
				else if (c == 'u') S = 102;//struct
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 102:
				if (c == 'c') S = 126;//struct
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 103:
				if (c == 'i') S = 105;//virtual
				else if (c == 'o') S = 104;//void, volatile
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 104:
				if (c == 'i') S = 12;//void
				else if (c == 'l') S = 181;//volatile
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 105:
				if (c == 'r') S = 106;//virtual
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 106:
				if (c == 't') S = 107;//virtual
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 107:
				if (c == 'u') S = 108;//virtual
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 108:
				if (c == 'a') S = 16;//virtual
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 109:
				if (c == 'e') S = 110;//return
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 110:
				if (c == 't') S = 111;//return
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 111:
				if (c == 'u') S = 112;//return
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 112:
				if (c == 'r') S = 113;//return
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 113:
				if (c == 'n') S = 3;//найдено return, cin
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 114:
				if (c == 'e') S = 116;//template
				else if (c == 'h') S = 119;//this, throw
				else if (c == 'r') S = 115;//true, try
				else if (c == 'y') S = 121;//typename
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 115:
				if (c == 'u') S = 24;//true
				else if (c == 'y') S = 3;//найдено try
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 116:
				if (c == 'm') S = 117;//template
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 117:
				if (c == 'p') S = 118;//template
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 118:
				if (c == 'l') S = 79;//template
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 119:
				if (c == 'i') S = 34;//this
				else if (c == 'r') S = 120;//throw
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 120:
				if (c == 'o') S = 70;//throw
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 121:
				if (c == 'p') S = 122;//typename
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 122:
				if (c == 'e') S = 123;//typename
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 123:
				if (c == 'n') S = 124;//typename
				else if (c == 'i') S = 12;//typeid
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 124:
				if (c == 'a') S = 125;//typename
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 125:
				if (c == 'm') S = 82;//typename
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
				//запись структуры
			case 126:
				if (c == 't') S = 3;//struct
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
				//целые константы
			case 128:
				if (separators(&c) || comparison_operators(&c) || operation_sings(&c) || skip(&c)) S = 5;//введена константа
				else if (numbers(&c) || c == '0') S = 128;//вводится константа
				else if (c == '.') S = 129;//вводится дробная константа
				else if (c == 'e' || c == 'E') S = 59;//экспонента
				else S = 130;
				break;
			case 129:
				if (numbers(&c) || c == '0') S = 131;//вводится дробная константа
				else S = 130;//ошибка
				break;
				//ошибка константы
			case 130:
				if (separators(&c) || comparison_operators(&c) || operation_sings(&c) || skip(&c)) S = 6;
				break;
				//вводится дробная константа
			case 131:
				if (end_condition(&c)) S = 5;//введена константа
				else if (numbers(&c) || c == '0') S = 131;//вводится дробная константа
				else if (c == 'e' || c == 'E') S = 59;//экспонента
				else S = 130;//ошибка
				break;
				//вводится 0-константа
			case 132:
				if (separators(&c) || comparison_operators(&c) || operation_sings(&c) || skip(&c)) S = 5;//введена 0-константа
				else if (c == '.') S = 129;//вводится дробная 0-константа
				else S = 130;
				break;
				//запись :: { } ( ) # . , ;
			case 135:
				S = 0;
				break;
				//разделители
				//чтение ::
			case 136:
				if (c == ':') S = 135;
				else S = 137;
				break;
				//запись :
			case 137:
				S = 0;
				break;
				//операторы сравнения
				//запись <= >= != == ? || &&
			case 139:
				S = 0;
				break;
				//запись < >
			case 140:
				S = 0;
				break;
				//чтение ==
			case 141:
				if (c == '=') S = 139;
				else S = 151;
				break;
				//чтение < <= <<
			case 142:
				if (c == '=') S = 139;
				else if (c == '<') S = 152;
				else S = 140;
				break;
				//чтение > >= >>
			case 143:
				if (c == '=') S = 139;
				else if (c == '>') S = 152;
				else S = 140;
				break;
				//знаки операций
				//чтение + ++ +=
			case 144:
				if (c == '+' || c == '=') S = 152;
				else S = 151;
				break;
				//чтение- -- -=
			case 145:
				if (c == '-' || c == '=') S = 152;
				else S = 151;
				break;
				//чтение * *=
			case 146:
				if (c == '=') S = 152;
				else S = 151;
				break;
				//чтение / /=
			case 147:
				if (c == '=') S = 152;
				else S = 151;
				break;
				//чтение % %=
			case 148:
				if (c == '=') S = 152;
				else S = 151;
				break;
				//чтение | || |=
			case 149:
				if (c == '|') S = 139;
				else if (c == '=') S = 152;
				else S = 151;
				break;
				//чтение & && &=
			case 150:
				if (c == '&') S = 139;
				else if (c == '=') S = 152;
				else S = 151;
				break;
				//запись + - / * = | &
			case 151:
				S = 0;
				break;
				//запись ++ += -- -= * *= / /= % %= |= &= >> <<
			case 152:
				S = 0;
				break;
				//запись строки
			case 153:
				S = 0;
				break;
				//чтение ! !=
			case 154:
				if (c == '=') S = 139;
				else S = 151;
				break;
				//ошибка идентефикатора
				//чтение слова
			case 155:
				if (end_condition(&c)) S = 156;
				break;
				//запись слова
			case 156:
				S = 0;
				break;
			case 157:
				if (c == 'l') S = 158;//include
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 158:
				if (c == 'u') S = 159;//include
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 159:
				if (c == 'd') S = 24;//include
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 160:
				if (c == 'u') S = 161;//double
				else if (end_condition(&c)) S = 2;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 161:
				if (c == 'b') S = 53;//double
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 162:
				if (c == 'c') S = 163;//wchar_t
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 163:
				if (c == 'h') S = 164;//wchar_t
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 164:
				if (c == 'a') S = 165;//wchar_t
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 165:
				if (c == 'r') S = 98;//wchar_t
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 166:
				if (c == 'c') S = 167;//protected
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 167:
				if (c == 't') S = 168;//protected
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 168:
				if (c == 'e') S = 12;//protected, unsigned
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 169:
				if (c == 'i') S = 170;//union
				else if (c == 's') S = 174;//unsigned
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 170:
				if (c == 'o') S = 171;//union
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 171:
				if (c == 'n') S = 3;//union
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 174:
				if (c == 'i') S = 175;//unsigned
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 175:
				if (c == 'g') S = 176;//unsigned
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 176:
				if (c == 'n') S = 168;//unsigned
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 177:
				if (c == 'l') S = 178;//nullptr
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 178:
				if (c == 'l') S = 179;//nullptr
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 179:
				if (c == 'p') S = 180;//nullptr
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 180:
				if (c == 't') S = 30;//nullptr
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 181:
				if (c == 'a') S = 182;//volatile
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 182:
				if (c == 't') S = 183;//volatile
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
			case 183:
				if (c == 'i') S = 53;//volatile
				else if (end_condition(&c)) S = 0;
				else if (allowed_character(&c)) S = 1; else S = 155;
				break;
				//вводится экспонента
			case 184:
				if (!(numbers(&c) || c == '0')) S = 5;
				else if (end_condition(&c)) S = 130;
				break;

			default: MessageBox::Show(L"Ошибка состояний!", L"Программа остановлена", MessageBoxButtons::OK, MessageBoxIcon::Error);
				return;
			}

			if (c == '\n')
				line++;

			if (S != 0 && S != 2 && S != 5 && S != 6 && S != 133 && S != 137 && S != 140 && S != 151 && S != 156 && S != 173)
				word += c;

			if (S == 0 && word != "")//если нет этого слова в других категориях
				add_id(&word, row_id, row_keyword), last_s == 1 ? i-- : 0;
			else if (S == 2)
				add_keyword(&word, row_keyword);
			else if (S == 5)
				add_const(&word, row_const);
			else if ((S == 139 || S == 140))
				add_comparison_operators(&word, row_comparison_operators);
			else if ((S == 151 || S == 152))
				add_operation_sings(&word, row_operation_sings);
			else if ((S == 135 || S == 137))
				add_separators(&word, row_separators);
			else if (S == 6)
				error(&word, "const", &line);
			else if (S == 156)
				error(&word, "id", &line);

			if (S == 6 || S == 135 || S == 139 || S == 152 || S == 153 || S == 156) i--;//предпросмотр для занесения слова без чтения символа
			else if (S == 2 || S == 5 || S == 137 || S == 140 || S == 151) i -= 2;
		}
	}
	else MessageBox::Show(L"Пустая последовательность!", L"Программа остановлена", MessageBoxButtons::OK, MessageBoxIcon::Error);
}
void TA2coursework::MyForm::code_descriptor(int* row_key_word, int* row_id, int* row_const, int* row_comparison_operators, int* row_operation_sings, int* row_separators)
{
	StreamReader^ fout = gcnew StreamReader("buffer.txt");
	String^ str = fout->ReadToEnd();
	fout->Close();
	StreamWriter^ fcd = gcnew StreamWriter("code_descriptor.txt", false);
	String^ word = "";
	String^ out = "";
	wchar_t c;
	for (int i = 0; i < str->Length; i++) {
		c = str[i];

		if (c == '"')
		{
			do word += c; while (++i < str->Length && (c = str[i]) != '"');
			word += c;
		}
		else if (numbers(&c) || c == '0')
		{
			do {
				word += c;
				if (++i < str->Length) c = str[i];
			} while (i < str->Length && (numbers(&c) || c == '0' || c == '.'));

			if (c == 'e' || c == 'E') {
				word += c;
				if (i + 1 < str->Length) {
					c = str[++i];
					if ((c == '+' || c == '-') && ++i < str->Length) {
						word += c;
						c = str[i];
						do {
							word += c;
							if (++i < str->Length) c = str[i];
						} while (i < str->Length && (numbers(&c) || c == '0'));
					}
					else i--;
				}
			}
			else i--;
		}
		else if (!end_condition(&c))//любой другой символ не разделитель
		{
			do word += c; while (++i < str->Length && !end_condition(&(c = str[i])));
			i--;
		}
		else if (comparison_operators(&c))
		{
			word += c;
			if (++i < str->Length && comparison_operators(&(c = str[i]))) word += c;
			else i--;
		}
		else if (other(&c))
		{
			word += c;
		}
		else if (operation_sings(&c))
		{
			word += c;
			if (++i < str->Length && operation_sings(&(c = str[i])))
				word += c;
			else i--;
		}
		else if (separators(&c))
		{
			word += c;
		}

		//вывод дескрипторного кода
		if (word != "") {
			for (size_t j = 0; j < *row_key_word; j++)
				if (word == System::Convert::ToString(table_lexeme->Rows[j]->Cells[1]->Value))
					out = "(" + Column10->HeaderCell->Value + "," + (j + 1) + ")";

			for (size_t j = 0; j < *row_id; j++)
				if (word == System::Convert::ToString(table_id->Rows[j]->Cells[1]->Value))
					out = "(" + Column20->HeaderCell->Value + "," + (j + 1) + ")";

			for (size_t j = 0; j < *row_const; j++)
				if (word == System::Convert::ToString(table_const->Rows[j]->Cells[1]->Value))
					out = "(" + Column30->HeaderCell->Value + "," + (j + 1) + ")";

			for (size_t j = 0; j < *row_comparison_operators; j++)
				if (word == System::Convert::ToString(table_comparison_operators->Rows[j]->Cells[1]->Value))
					out = "(" + Column40->HeaderCell->Value + "," + (j + 1) + ")";

			for (size_t j = 0; j < *row_operation_sings; j++)
				if (word == System::Convert::ToString(table_operation_sings->Rows[j]->Cells[1]->Value))
					out = "(" + Column50->HeaderCell->Value + "," + (j + 1) + ")";

			for (size_t j = 0; j < *row_separators; j++)
				if (word == System::Convert::ToString(table_separators->Rows[j]->Cells[1]->Value))
					out = "(" + Column60->HeaderCell->Value + "," + (j + 1) + ")";

			if (out != "") fcd->Write(out + " "), out = "";
			word = "";
		}
	}
	fcd->Close();
	StreamReader^ fcdout = gcnew StreamReader("code_descriptor.txt");
	descriptor_code->Text = fcdout->ReadToEnd();
	fcdout->Close();
}
void TA2coursework::MyForm::code_pre(int* row_key_word, int* row_id, int* row_const, int* row_comparison_operators, int* row_operation_sings, int* row_separators)
{
	StreamReader^ fcdout = gcnew StreamReader("code_descriptor.txt");
	String^ str = fcdout->ReadToEnd();
	fcdout->Close();
	StreamWriter^ fcp = gcnew StreamWriter("code_pre.txt", false);
	String^ table = "";
	String^ number = "";
	String^ pre_word = "";
	int int_number;
	for (int i = 0; i < str->Length; i++) {
		if (str[i] == '(') {
			i++;
			while (str[i] != ',' && str[i])
				table += str[i++];
			i++;
			while (str[i] != ')' && str[i])
				number += str[i++];

			int_number = System::Convert::ToInt32(number) - 1;

			if (table == System::Convert::ToString(Column10->HeaderCell->Value))
				pre_word = System::Convert::ToString(table_lexeme->Rows[int_number]->Cells[2]->Value);
			else if (table == System::Convert::ToString(Column20->HeaderCell->Value))
				pre_word = System::Convert::ToString(table_id->Rows[int_number]->Cells[2]->Value);
			else if (table == System::Convert::ToString(Column30->HeaderCell->Value))
				pre_word = System::Convert::ToString(table_const->Rows[int_number]->Cells[2]->Value);
			else if (table == System::Convert::ToString(Column40->HeaderCell->Value))
				pre_word = System::Convert::ToString(table_comparison_operators->Rows[int_number]->Cells[2]->Value);
			else if (table == System::Convert::ToString(Column50->HeaderCell->Value))
				pre_word = System::Convert::ToString(table_operation_sings->Rows[int_number]->Cells[2]->Value);
			else if (table == System::Convert::ToString(Column60->HeaderCell->Value))
				pre_word = System::Convert::ToString(table_separators->Rows[int_number]->Cells[2]->Value);

			fcp->Write(pre_word + " ");
			pre_word = "";
			table = "";
			number = "";
		}
	}
	fcp->Close();
	StreamReader^ fcpout = gcnew StreamReader("code_pre.txt");
	pre_code->Text = fcpout->ReadToEnd();
	fcpout->Close();
}
System::Void TA2coursework::MyForm::run_Click(System::Object^ sender, System::EventArgs^ e)
{
	list_of_erros->Text = "";
	String^ str = text_in->Text;
	cleaning(&str);
	int row_key_word = 1, row_id = 1, row_const = 1, row_comparison_operators = 1, row_operation_sings = 1, row_separators = 1;
	//создание таблиц
	{
		table_id->RowCount = 1;
		table_lexeme->RowCount = 1;
		table_const->RowCount = 1;
		table_comparison_operators->RowCount = 1;
		table_operation_sings->RowCount = 1;
		table_separators->RowCount = 1;

		table_id->RowCount = row_id + 1;
		table_id->ColumnCount = 3;
		table_id->Rows[0]->Cells[0]->Value = "";
		table_id->Rows[0]->Cells[1]->Value = "";
		table_id->Rows[0]->Cells[2]->Value = "";

		table_lexeme->RowCount = row_key_word + 1;
		table_lexeme->ColumnCount = 3;
		table_lexeme->Rows[0]->Cells[0]->Value = "";
		table_lexeme->Rows[0]->Cells[1]->Value = "";
		table_lexeme->Rows[0]->Cells[2]->Value = "";

		table_const->RowCount = row_const + 1;
		table_const->ColumnCount = 3;
		table_const->Rows[0]->Cells[0]->Value = "";
		table_const->Rows[0]->Cells[1]->Value = "";
		table_const->Rows[0]->Cells[2]->Value = "";

		table_comparison_operators->RowCount = row_comparison_operators + 1;
		table_comparison_operators->ColumnCount = 3;
		table_comparison_operators->Rows[0]->Cells[0]->Value = "";
		table_comparison_operators->Rows[0]->Cells[1]->Value = "";
		table_comparison_operators->Rows[0]->Cells[2]->Value = "";

		table_operation_sings->RowCount = row_operation_sings + 1;
		table_operation_sings->ColumnCount = 3;
		table_operation_sings->Rows[0]->Cells[0]->Value = "";
		table_operation_sings->Rows[0]->Cells[1]->Value = "";
		table_operation_sings->Rows[0]->Cells[2]->Value = "";

		table_separators->RowCount = row_separators + 1;
		table_separators->ColumnCount = 3;
		table_separators->Rows[0]->Cells[0]->Value = "";
		table_separators->Rows[0]->Cells[1]->Value = "";
		table_separators->Rows[0]->Cells[2]->Value = "";
	}
	processing(&row_key_word, &row_id, &row_const, &row_comparison_operators, &row_operation_sings, &row_separators);
	code_descriptor(&row_key_word, &row_id, &row_const, &row_comparison_operators, &row_operation_sings, &row_separators);
	code_pre(&row_key_word, &row_id, &row_const, &row_comparison_operators, &row_operation_sings, &row_separators);
}