   M S B A M L   `   `   `  ����Z  VEmployee Data Management System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:Employee_Data_Management_SystemEmployee_Data_Management_System  5   	   0    *Employee_Data_Management_System.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:Employee_Data_Management_System     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � -   5         �Name$ mainMenuGrid��5	       � ColumnDefinitions 5
   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��25 �6   �� 5      $��20 �6   .+�5   
   �� -   5         ��Name$ 
btnOpenCsv��5      $��	Open File��6%   $��1��69   $��1��6I   6V   �� 5   
   -   5         ��Name$ txtSearchEmployee��5      $��1��6-   $��2��6:   $��2��6J   $��	10 0 10 0q�6�   �� 5    
   -   5         ��Name$ cmbOrderEmployee��5       $��4��6-   $��1��6=   .@�5!      �� .��6   
First Name6'   �� 5"      .��6   Age6    �� 5#      .��6   
Department6'   �� 5$      .��6   Salary6#   5%      �� 5&   
   -   5         ��Name$ lvEmployees��5&      $��2��6)   $��1��66   $��4��6F   $��3��6Z   $��	0 10 0 10q�6k    �� View 5(   
   �� 5*      	 �� AllowsColumnReorder$
	 true��6   
 �� ColumnHeaderToolTip$
 Employee Information��62   .P�5+      �� $��
First Name��6G    �� Width$	 200��6[    �� DisplayMemberBinding 6!   �� *Name+6[   �� 5-      $	 200��6J   $	��Age��6V    6%   �� *Age+6V   �� 5/      $��
Department��6Q   $	 200��6e    6%   �� *
Department+6e   �� 50      $��Salary��6I   $	 200��6Y    6!   �� *Salary+6Y   51      54      �� 55   
   $��1��6   $��5��6%   .+�56      �� -   5         ��Name$ txtAverageSalary��56      $��12q�62   �� 57      -   5        $ txtHighestSalary��57      $��12q�62   58      �� 5;   
   -   5        $ 	btnUpdate��5;      $��Update��6$   $��3��65   $��5��6E   $	��Top=�6S   $	��150��6k   6w   �� 5<   
   -	   5        $ 	btnDelete��5<      $��Delete��6$   $��4��65   $��5��6E   $	��Top=�6S   $	��150��6l   6x   �� 5=   
   -
   5        $ btnExit��5=      $��4��68   $��6��6H   $
��Exit��6U   5>      5?      