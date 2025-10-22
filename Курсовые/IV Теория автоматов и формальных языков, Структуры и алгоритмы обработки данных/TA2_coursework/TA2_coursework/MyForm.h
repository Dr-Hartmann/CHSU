#pragma once
namespace TA2coursework {
	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	public ref class MyForm : public System::Windows::Forms::Form
	{
	public:
		MyForm(void)
		{
			InitializeComponent();
		}
	protected:
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Button^ run;
	private: System::Windows::Forms::DataGridView^ table_id;
	private: System::Windows::Forms::DataGridView^ table_lexeme;
	private: System::Windows::Forms::DataGridView^ table_const;
	private: System::Windows::Forms::DataGridView^ table_comparison_operators;
	private: System::Windows::Forms::DataGridView^ table_operation_sings;
	private: System::Windows::Forms::DataGridView^ table_separators;
	private: System::Windows::Forms::RichTextBox^ text_out;
	private: System::Windows::Forms::RichTextBox^ descriptor_code;
	private: System::Windows::Forms::RichTextBox^ pre_code;
	private: System::Windows::Forms::RichTextBox^ list_of_erros;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column20;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column21;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column22;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column30;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column31;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column32;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column40;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column41;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column42;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column50;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column51;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column52;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column60;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column61;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column62;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column10;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column11;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Column12;
	private: System::Windows::Forms::RichTextBox^ text_in;
	private:
		//поиск идентификаторов
		bool search_id(String^*, int*);
		//добавление идентификаторов
		void add_id(String^*, int*, int*);
		//поиск лексем
		bool search_keyword(String^*, int*);
		//добавлние лексем
		void add_keyword(String^*, int*);
		//поиск констант
		bool search_const(String^*, int*);
		//добавление констант
		void add_const(String^*, int*);
		//поиск операторов сравнения
		bool search_comparison_operators(String^*, int*);
		//добавление операторов сравнения
		void add_comparison_operators(String^*, int*);
		//поиск знаков операций
		bool search_operation_sings(String^*, int*);
		//добавление знаков операций
		void add_operation_sings(String^*, int*);
		//поиск разделителей
		bool search_separators(String^*, int*);
		//добавление разделителей
		void add_separators(String^*, int*);

		//поиск скачков
		bool skip(wchar_t*);
		//поиск оператора сравнения
		bool comparison_operators(wchar_t*);
		//поиск знаков операций
		bool operation_sings(wchar_t*);
		//спец символы
		bool separators(wchar_t*);
		//другое
		bool other(wchar_t*);
		//поиск стоп-символа
		bool end_condition(wchar_t*);
		//разрешённые элементы имён переменных
		bool allowed_character(wchar_t*);
		//числа, не включая 0
		bool numbers(wchar_t*);
		//вывод сообщения об ошибке
		void error(String^*, String^, int*);

	private:
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		void InitializeComponent(void)
		{
			System::ComponentModel::ComponentResourceManager^ resources = (gcnew System::ComponentModel::ComponentResourceManager(MyForm::typeid));
			this->run = (gcnew System::Windows::Forms::Button());
			this->table_id = (gcnew System::Windows::Forms::DataGridView());
			this->Column20 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Column21 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Column22 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->table_lexeme = (gcnew System::Windows::Forms::DataGridView());
			this->table_const = (gcnew System::Windows::Forms::DataGridView());
			this->Column30 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Column31 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Column32 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->table_comparison_operators = (gcnew System::Windows::Forms::DataGridView());
			this->Column40 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Column41 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Column42 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->table_operation_sings = (gcnew System::Windows::Forms::DataGridView());
			this->Column50 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Column51 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Column52 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->table_separators = (gcnew System::Windows::Forms::DataGridView());
			this->Column60 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Column61 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Column62 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->text_out = (gcnew System::Windows::Forms::RichTextBox());
			this->text_in = (gcnew System::Windows::Forms::RichTextBox());
			this->descriptor_code = (gcnew System::Windows::Forms::RichTextBox());
			this->pre_code = (gcnew System::Windows::Forms::RichTextBox());
			this->list_of_erros = (gcnew System::Windows::Forms::RichTextBox());
			this->Column10 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Column11 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Column12 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->table_id))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->table_lexeme))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->table_const))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->table_comparison_operators))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->table_operation_sings))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->table_separators))->BeginInit();
			this->SuspendLayout();
			// 
			// run
			// 
			this->run->Location = System::Drawing::Point(930, 605);
			this->run->Name = L"run";
			this->run->Size = System::Drawing::Size(192, 33);
			this->run->TabIndex = 2;
			this->run->Text = L"Старт";
			this->run->UseVisualStyleBackColor = true;
			this->run->Click += gcnew System::EventHandler(this, &MyForm::run_Click);
			// 
			// table_id
			// 
			this->table_id->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->table_id->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(3) {
				this->Column20, this->Column21,
					this->Column22
			});
			this->table_id->Location = System::Drawing::Point(318, 308);
			this->table_id->Name = L"table_id";
			this->table_id->RowHeadersWidth = 51;
			this->table_id->Size = System::Drawing::Size(300, 162);
			this->table_id->TabIndex = 4;
			// 
			// Column20
			// 
			this->Column20->HeaderText = L"20";
			this->Column20->MinimumWidth = 6;
			this->Column20->Name = L"Column20";
			this->Column20->Width = 50;
			// 
			// Column21
			// 
			this->Column21->HeaderText = L"Идентификаторы";
			this->Column21->MinimumWidth = 6;
			this->Column21->Name = L"Column21";
			this->Column21->Width = 105;
			// 
			// Column22
			// 
			this->Column22->HeaderText = L"Псевдокод";
			this->Column22->MinimumWidth = 6;
			this->Column22->Name = L"Column22";
			this->Column22->Width = 70;
			// 
			// table_lexeme
			// 
			this->table_lexeme->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->table_lexeme->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(3) {
				this->Column10,
					this->Column11, this->Column12
			});
			this->table_lexeme->Location = System::Drawing::Point(12, 308);
			this->table_lexeme->Name = L"table_lexeme";
			this->table_lexeme->RowHeadersWidth = 51;
			this->table_lexeme->Size = System::Drawing::Size(300, 162);
			this->table_lexeme->TabIndex = 5;
			// 
			// table_const
			// 
			this->table_const->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->table_const->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(3) {
				this->Column30,
					this->Column31, this->Column32
			});
			this->table_const->Location = System::Drawing::Point(624, 308);
			this->table_const->Name = L"table_const";
			this->table_const->RowHeadersWidth = 51;
			this->table_const->Size = System::Drawing::Size(300, 162);
			this->table_const->TabIndex = 6;
			// 
			// Column30
			// 
			this->Column30->HeaderText = L"30";
			this->Column30->MinimumWidth = 6;
			this->Column30->Name = L"Column30";
			this->Column30->Width = 50;
			// 
			// Column31
			// 
			this->Column31->HeaderText = L"Константы";
			this->Column31->MinimumWidth = 6;
			this->Column31->Name = L"Column31";
			this->Column31->Width = 90;
			// 
			// Column32
			// 
			this->Column32->HeaderText = L"Псевдокод";
			this->Column32->MinimumWidth = 6;
			this->Column32->Name = L"Column32";
			this->Column32->Width = 85;
			// 
			// table_comparison_operators
			// 
			this->table_comparison_operators->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->table_comparison_operators->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(3) {
				this->Column40,
					this->Column41, this->Column42
			});
			this->table_comparison_operators->Location = System::Drawing::Point(12, 476);
			this->table_comparison_operators->Name = L"table_comparison_operators";
			this->table_comparison_operators->RowHeadersWidth = 51;
			this->table_comparison_operators->Size = System::Drawing::Size(300, 162);
			this->table_comparison_operators->TabIndex = 7;
			// 
			// Column40
			// 
			this->Column40->HeaderText = L"40";
			this->Column40->MinimumWidth = 6;
			this->Column40->Name = L"Column40";
			this->Column40->Width = 50;
			// 
			// Column41
			// 
			this->Column41->HeaderText = L"Операторы сравнения";
			this->Column41->MinimumWidth = 6;
			this->Column41->Name = L"Column41";
			this->Column41->Resizable = System::Windows::Forms::DataGridViewTriState::False;
			this->Column41->Width = 90;
			// 
			// Column42
			// 
			this->Column42->HeaderText = L"Псевдокод";
			this->Column42->MinimumWidth = 6;
			this->Column42->Name = L"Column42";
			this->Column42->Width = 85;
			// 
			// table_operation_sings
			// 
			this->table_operation_sings->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->table_operation_sings->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(3) {
				this->Column50,
					this->Column51, this->Column52
			});
			this->table_operation_sings->Location = System::Drawing::Point(318, 476);
			this->table_operation_sings->Name = L"table_operation_sings";
			this->table_operation_sings->RowHeadersWidth = 51;
			this->table_operation_sings->Size = System::Drawing::Size(300, 162);
			this->table_operation_sings->TabIndex = 8;
			// 
			// Column50
			// 
			this->Column50->HeaderText = L"50";
			this->Column50->MinimumWidth = 6;
			this->Column50->Name = L"Column50";
			this->Column50->Width = 50;
			// 
			// Column51
			// 
			this->Column51->HeaderText = L"Знаки операций";
			this->Column51->MinimumWidth = 6;
			this->Column51->Name = L"Column51";
			this->Column51->Width = 90;
			// 
			// Column52
			// 
			this->Column52->HeaderText = L"Псевдокод";
			this->Column52->MinimumWidth = 6;
			this->Column52->Name = L"Column52";
			this->Column52->Width = 85;
			// 
			// table_separators
			// 
			this->table_separators->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->table_separators->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(3) {
				this->Column60,
					this->Column61, this->Column62
			});
			this->table_separators->Location = System::Drawing::Point(624, 476);
			this->table_separators->Name = L"table_separators";
			this->table_separators->RowHeadersWidth = 51;
			this->table_separators->Size = System::Drawing::Size(300, 162);
			this->table_separators->TabIndex = 9;
			// 
			// Column60
			// 
			this->Column60->HeaderText = L"60";
			this->Column60->MinimumWidth = 6;
			this->Column60->Name = L"Column60";
			this->Column60->Width = 50;
			// 
			// Column61
			// 
			this->Column61->HeaderText = L"Разделители";
			this->Column61->MinimumWidth = 6;
			this->Column61->Name = L"Column61";
			this->Column61->Width = 90;
			// 
			// Column62
			// 
			this->Column62->HeaderText = L"Псевдокод";
			this->Column62->MinimumWidth = 6;
			this->Column62->Name = L"Column62";
			this->Column62->Width = 85;
			// 
			// text_out
			// 
			this->text_out->Location = System::Drawing::Point(369, 12);
			this->text_out->Name = L"text_out";
			this->text_out->Size = System::Drawing::Size(351, 290);
			this->text_out->TabIndex = 13;
			this->text_out->Text = L"";
			// 
			// text_in
			// 
			this->text_in->Location = System::Drawing::Point(12, 12);
			this->text_in->Name = L"text_in";
			this->text_in->Size = System::Drawing::Size(351, 290);
			this->text_in->TabIndex = 14;
			this->text_in->Text = resources->GetString(L"text_in.Text");
			// 
			// descriptor_code
			// 
			this->descriptor_code->Location = System::Drawing::Point(726, 12);
			this->descriptor_code->Name = L"descriptor_code";
			this->descriptor_code->Size = System::Drawing::Size(397, 135);
			this->descriptor_code->TabIndex = 15;
			this->descriptor_code->Text = L"";
			// 
			// pre_code
			// 
			this->pre_code->Location = System::Drawing::Point(726, 153);
			this->pre_code->Name = L"pre_code";
			this->pre_code->Size = System::Drawing::Size(397, 149);
			this->pre_code->TabIndex = 16;
			this->pre_code->Text = L"";
			// 
			// list_of_erros
			// 
			this->list_of_erros->Location = System::Drawing::Point(930, 307);
			this->list_of_erros->Name = L"list_of_erros";
			this->list_of_erros->Size = System::Drawing::Size(193, 292);
			this->list_of_erros->TabIndex = 17;
			this->list_of_erros->Text = L"";
			// 
			// Column10
			// 
			this->Column10->HeaderText = L"10";
			this->Column10->MinimumWidth = 6;
			this->Column10->Name = L"Column10";
			this->Column10->Width = 50;
			// 
			// Column11
			// 
			this->Column11->HeaderText = L"Ключевые слова";
			this->Column11->MinimumWidth = 6;
			this->Column11->Name = L"Column11";
			this->Column11->Width = 90;
			// 
			// Column12
			// 
			this->Column12->HeaderText = L"Псевдокод";
			this->Column12->MinimumWidth = 6;
			this->Column12->Name = L"Column12";
			this->Column12->Width = 85;
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(1131, 650);
			this->Controls->Add(this->list_of_erros);
			this->Controls->Add(this->pre_code);
			this->Controls->Add(this->descriptor_code);
			this->Controls->Add(this->text_in);
			this->Controls->Add(this->text_out);
			this->Controls->Add(this->table_separators);
			this->Controls->Add(this->table_operation_sings);
			this->Controls->Add(this->table_comparison_operators);
			this->Controls->Add(this->table_const);
			this->Controls->Add(this->table_lexeme);
			this->Controls->Add(this->table_id);
			this->Controls->Add(this->run);
			this->Name = L"MyForm";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"MyForm";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->table_id))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->table_lexeme))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->table_const))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->table_comparison_operators))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->table_operation_sings))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->table_separators))->EndInit();
			this->ResumeLayout(false);

		}
#pragma endregion
		//удаление пробелов и пустых строк
	private: void cleaning(String^*);
		   //чтение лексем, идентификаторов, констант, операторов сравнения, знаков операций, разделителей
	private: void processing(int*, int*, int*, int*, int*, int*);
		   //формирование дескрипторного кода
	private: void code_descriptor(int*, int*, int*, int*, int*, int*);
		   //формирование псевдокода
	private: void code_pre(int*, int*, int*, int*, int*, int*);
		   //старт
	private: System::Void run_Click(System::Object^ sender, System::EventArgs^ e);
	};
}
