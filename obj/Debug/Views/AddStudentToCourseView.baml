   M S B A M L   `   `   `  ����I  EOpenUniversity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:OpenUniversity.ViewsOpenUniversity.Views  5   	   1    +OpenUniversity.Views.AddStudentToCourseView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:OpenUniversity.Views     5   	     � Title$  AddStudentToCourseView��5   	   $	��450��6(   $	��800��65   .��5	      � .+�5
   
   �� $��Vælg kursus��6   $
��Left=�6&   $��
121,89,0,0q�6A   $	��Top=�6U   �� 5   
    �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$ Id��5      $
��Left=�5      $��121,129,0,0q�6(   $	��Top=�6=   $	��120��6U   ��5      �� *	Courses+ �� SelectedItem 5      �� *CurrentCourse+ �� Mode$ TwoWay=�5   U   �� 5   
   $��Vælg studerende��6   $
��Left=�6*   $��121,154,0,0q�6E   $	��Top=�6Z   �� 5   
   $
��Left=�5      $��92��6(   $��121,189,0,0q�64   $	��Top=�6I   $	��120��6a   ��5      �� *
Students+��5      �� 5      .R�5      �� ��6   �� *
FullName+ d� 	IsChecked 5      �� *AttendingCourse+$ TwoWay=�5      5      �� 5   
   $��Opdater��5      $
��Left=�6   $��165,299,0,0q�6:   $	��Top=�6O   $��75��6g   ��5      �� *Update+5   g   �� 5    
   $��Status��6   $
��Left=�6!   $��
88,338,0,0q�6<   $	��Top=�6P   �� 5!   
   $
��Left=�5%      $��165,338,0,0q�6+   $	��Top=�6@   ��5"      �� *Status+y�5#      �� *StatusVisibility+5%   @   5)      5*      