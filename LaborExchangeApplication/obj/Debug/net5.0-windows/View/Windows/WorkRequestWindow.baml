   M S B A M L   `   `   `  ����S  OLaborExchangeApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:LaborExchangeApplication.View.Windows%LaborExchangeApplication.View.Windows  5   	   W0clr-namespace:LaborExchangeApplication.ViewModel"LaborExchangeApplication.ViewModel  5   	   =    7LaborExchangeApplication.View.Windows.WorkRequestWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Elocal3clr-namespace:LaborExchangeApplication.View.Windows     5   	   F	viewmodel0clr-namespace:LaborExchangeApplication.ViewModel     5   	     � Title$  Work requests��5	   	   $	��700��6   $	��950��6,   $	��650��68   $	��360��6H    � WindowStartupLocation$ CenterScreen=�6W      WindowStyle#��[  5
   	    � DataContext 5      =   7LaborExchangeApplication.ViewModel.WorkRequestViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��350 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��70 �6   �� 5      $��* �6   �� 5      $��60 �6   .+�5   
   �� $��0q�6     BorderStyle#��[ 6%   $��2��6J   ._�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��5q�6   $?��9pack://application:,,,/Assets/Images/applicationImage.png��6#   �� 5      $��ILabor Exchange��6     TitleTextBlockStyle#��[ 63   5      5       �� 5"   
   $��1��6   $��40��6!   $��Right=�6-   $��5q�6I     ColorSwitchImageButtonStyle#��[ 6T   ��5#      �� *SwitchColorModeCommand+�� 5%   
   $��1��6   .+�5&      �� $#��Добавить заявку��6   #��[ 6/   �� 5'      $��Должность��6     TextBlockRowStyle#��[ 6)   �� 5(        ComboBoxRowStyle#��[ 6    �� DisplayMemberPath$ Value��5)      ��5(   A   �� *Professions+ �� SelectedItem 6e   �� *SelectedProfession+5)      �� 5*      $:��4Требования по зарплате (руб.)��6   #��[ 6=   �� 5+        TextBoxRowStyle#��[ 6:   ��6   �� *SalaryRequirements+6:   �� 5,      $��Рабочий день��6   #��[ 6,   �� 5-      #��[ 6   $ Value��5.      ��5-   A   �� *WorkDayRequirements+ 6m   �� *SelectedWorkDayRequirement+5.      �� 5/      $"��Доп. информация��6   #��[ 6/   �� 50       � ToolTip$` ZУкажите дополнительные особенности вашей заявки��6,   	 �� TextWrapping$
	 Wrap=�6f   
 �� AcceptsReturn$

 True��6z    �� 
AcceptsTab$
 True��51       �� 	IsEnabled$
 True��6(   $	��100��6D   #��[ 6Q   ��50      �� *Info+51   Q   �� 52      $��Добавить��6     ButtonStyle#��[ 6(   ��6M   �� *AddJobRequestCommand+53      �� 55   
   $��1��6   $��1��6$   .��56      �� $��10q�6   .+�57      �� #��[ 6   ._�58      �� $��Мои заявки��6    $	��5,0q�62     TextBlockStyle#��[ 6?   59      �� 5:       	 ListBoxStyle#��[	 6j   ��6   �� *JobRequests+ 6>   �� *SelectedJobRequest+��5;      �� 5<      .R�5=      �� .+�5>   "   �� $��10q�6)   #��[ 65    �� CornerRadius$ 5��6Z   ._�5?   &   �� $��10q�61   .+�5@   *   �� #��[ 6T   $��22(�6   $
��Bold"�6�   ��64   �� *ProfessionName+6�   �� 5A   *   #��[ 6t   ��64   �� *SalaryRequirements+ �� StringFormat$ Зарплата: {0}��6t   �� 5B   *   #��[ 6}   ��64   �� *WorkDayRequirementsName+$" Рабочий день: {0}��6}   �� 5C   *   �� 5D   *   #��[ 6J   ��64   �� *Info+6J   5E   '   5F   #   �� 5G   "   $��Удалить��6)   #��[ 6;   $	��120��5I   q   $
��Left=�6}   ��5H   )   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" �� �� Path$) #DataContext.DeleteJobRequestCommand���5I   T   �� 6}   5J      5K      5M      5N      5O      �� 5Q   
   $ 
Horizontal=�6   $��3��6.   $��2��6;   $��Center=�6O   .+�5R      �� $��
Назад��6   $��3��6&   $
��Left=�63   $��Center=�6N   $	��100��6i   #��[ 6u    �� 	IsDefault$
 True��5S      ��6$   �� *OpenApplicantWindowCommand+�� 5T      $��
Выход��6   $��3��6&   $
��Left=�63   $��Center=�6N   $	��100��6i   #��[ 6u   $
 True��5U      ��6$   �� *SignOutCommand+5V      5W      5X      