   M S B A M L   `   `   `  ����J  FmyStudyJournal2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:myStudyJournal2myStudyJournal2  5      "    myStudyJournal2.DatabasePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:myStudyJournal2     5      -   5          N� Title$  DatabasePage��5	      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5         AutoGenerateColumns .  $��10,60,10,9.6q�6Z      MaxColumnWidth$	 300Q�6p   $��Microsoft YaHei UI)�6�   ��6%   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 6%      Header$ Id��60      
FontFamily$ Microsoft YaHei UI)�6S   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *Id+6S     5      $	 160 6%   $
 Date��61   $ Microsoft YaHei UI)�6X    6?   �� *Date+6X     5      $	 200 6%   $ Title��61   $ Microsoft YaHei UI)�6Z    6@   �� *Title+6Z     5      $	 250 6%   $
 Tags��61   $ Microsoft YaHei UI)�6X    6?   �� *Tags+6X   4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Options��6)   $	 150 6;   	   CellTemplate	 5      �� 5      .R�5      � 
 � ColumnDefinitions
 5   "   �� 5   &   $��50 �67   �� 5   &   $��50 �67   �� 5   &   $��50 �67    � RowDefinitions 5   "   �� 5   &   $
��auto �64   .+�5   "   ��  ��Name$ viewBtn��6)   -   5        $��0��5   :   $
��View��6J   
������ $��Microsoft YaHei UI)�6�   �� 5    "   $ editBtn��6)   -   5        $��1��5    :   $
��Edit��6J   
������ $��Microsoft YaHei UI)�6�   �� 5!   "   $ 	deleteBtn��6)   -   5        $��2��5!   <   $��Delete��6L   
������ $��Microsoft YaHei UI)�6�   5"      5#      5%      5'      �� 5(   
   -   5         ��Name$ topicSelector��5(      $
��Left=�6+   $��
204,17,0,0q�6F   $	��Top=�6Z   $	��197��6r   $��Microsoft YaHei UI)�6~   �� 5)   
   $
��Tags��6   $
��Left=�6   $��
165,14,0,0q�6:   $	��Top=�6N   $��Microsoft YaHei UI)�6f   5*      5+      