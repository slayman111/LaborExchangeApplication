   M S B A M L   `   `   `  ����S  OLaborExchangeApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:LaborExchangeApplication.View.Windows%LaborExchangeApplication.View.Windows  5   	   W0clr-namespace:LaborExchangeApplication.ViewModel"LaborExchangeApplication.ViewModel  5   	   >    8LaborExchangeApplication.View.Windows.RegistrationWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Elocal3clr-namespace:LaborExchangeApplication.View.Windows     5   	   F	viewmodel0clr-namespace:LaborExchangeApplication.ViewModel     5   	     � Title$  Registration��5	   	   $	��900��6   $	��800��6+   $	��820��67   $	��550��6G    � WindowStartupLocation$ CenterScreen=�6V      WindowStyle#��[  5
   	    � DataContext 5      >   8LaborExchangeApplication.ViewModel.RegistrationViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $��70 �6   �� 5      $��35 �6   �� 5      $��* �6   �� 5      $��60 �6    � ColumnDefinitions 5   
   �� 5      $	��230 �6   �� 5      $��* �6   .+�5   
   �� $��0q�6     BorderStyle#��[ 6%   $��2��6J   ._�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $?��9pack://application:,,,/Assets/Images/applicationImage.png��6   $��5q�6[   �� 5      $��ILabor Exchange��6     TitleTextBlockStyle#��[ 63   5       5!      �� 5#   
   $��1��6   $��40��6!   $��Right=�6-   $��5q�6I     ColorSwitchImageButtonStyle#��[ 6T   ��5$      �� *SwitchColorModeCommand+�� 5&   
   $��1��6   $��2��6!   $Y��SЗаполните учетные данные для вашего аккаунта��65   $��Center=�6i   $��Center=�6�     TextBlockStyle#��[ 5'      �� 5)   
   $��2��6   .+�5*      �� $��ФИО��6     TextBlockRowStyle#��[ 6#   �� 5+      $��
Логин��6   #��[ 6%   �� 5,      $��Пароль��6   #��[ 6&   �� 5-      $%��Повторите пароль��6   #��[ 60   �� 5.      $��Дата рождения��6   $��32��6-   #��[ 69   �� 5/      $��Пол��6   #��[ 6#   �� 50      $��Email��6   #��[ 6%   �� 51      $��Телефон��6   #��[ 6'   �� 52      $.��(Деятельность на бирже��6   #��[ 65   �� 53      $��
Город��6   #��[ 6%   �� 54      $)��#Семейное положение��6   #��[ 62   �� 55      $%��Жилищные условия��6   #��[ 60   �� 56      $&�� Информация о себе��6   $	��100��61   #��[ 6>   �� 57      $��Аватар��6   $��96��6&   #��[ 62   58      �� 5:   
   $��2��6   $��1��6"   .+�5;      ��  � ToolTip$< 6Ваше полное имя через пробелы��60     TextBoxRowStyle#��[ 6X   ��6   �� *
Fullname+6X   �� 5<      $. (Логин вашего аккаунта��6-   #��[ 6M   ��6   �� *Login+6M   G� 5=      -   5         G�Name$ passwordBoxPassword��5=      $7 1Пароль для вашего аккаунта��67     PasswordBoxRowStyle#��[ 6\   5>      G� 5?      -   5        $ passwrodBoxRepeatPassword��5?      $E ?Введенные пароли должны совпадать��6=   #��[ 6i   5@      (  "System.Windows.Controls.DatePicker  5A      $��32��6   $7 1Укажите свою дату рождения��6%     DatePickerStyle#��[ 6l      SelectedDate 6J   �� *
BornDate+6l   �� 5B      $" Укажите ваш пол��6   	 �� DisplayMemberPath$	 Value��6Q    	 ComboBoxRowStyle#��[	 5C      ��5B   1   �� *	Genders+
 �� SelectedItem
 6k   �� *SelectedGender+5C      �� 5D      #��[ 6-   ��6   �� *Email+6-   �� 5E      $H BФорматы телефонного номера:
"+7..."
"8..."��6-   #��[ 5F      ��5E      �� *Phone+5F      �� 5G      $T NВ какой роли вы регистрируете свой аккаунт��6   $	 Value��6j   #��[	 5H   =   ��5G   L   �� *Roles+
 5H      �� *SelectedRole+6=   �� 5I      $, &Ваш город проживания��6   $	 Value��6U   #��[	 5J      ��5I   6   �� *Cities+
 6o   �� *SelectedCity+5J      �� 5K      $* $Ваш семейный статус��6   $	 Value��6\   #��[	 5L      ��5K   5   �� *FamilyStatuses+
 6v   �� *SelectedFamilyStatus+5L      �� 5M      $J DОпишите кратко ваши жилищные условия��69   #��[ 6h   ��6   �� *HousingConditions+6h   �� 5N      $Z TОпишите ваши профессиональные качества здесь��64    �� TextWrapping$
 Wrap=�6k    �� AcceptsReturn$
 True��6    �� 
AcceptsTab$
 True��5O       �� 	IsEnabled$
 True��6(   $	��100��6D   #��[ 6Q   ��5N      �� *PersonalInfo+5O   Q   �� 5P      $ 
Horizontal=�6   $
��15,5q�62   .+�5Q      �� $��94��6   $��94��6#   $
��Left=�6/   ��6J   �� *Avatar+ �� Mode$ TwoWay=��� 5R      $/��)Загрузить изображение��6   $��Center=�69   $x rВыберите картинку, которая будет отображаться в вашем профиле��5S       
 ButtonStyle#��[
 6a   ��5T      �� *UploadImageCommand+5U      5V      �� 5X   
   $��3��6   $ 
Horizontal=�6"   $��2��6;   .+�5Y      �� $��
Назад��6   $��Center=�6%   $	��100��6@   #��[
 6L   ��5Z      �� * OpenAuthorizationWindowCommand+�� 5[      $*��$Зарегистрироваться��6   $��Center=�62   $	��210��6M   #��[
 6Y   ��5\      �� *RegisterUserCommand+5]      5^      5_      