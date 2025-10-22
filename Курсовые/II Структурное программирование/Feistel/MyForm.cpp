#include "MyForm1.h"


using namespace System;
using namespace System::Windows::Forms;

[STAThread]
void main(cli::array<String^> ^ args) {

    Application::EnableVisualStyles();
    Application::SetCompatibleTextRenderingDefault(false);

    Feistel::MyForm1 form;
    Application::Run(% form);

}