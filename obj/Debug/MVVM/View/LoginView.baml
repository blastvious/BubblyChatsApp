   M S B A M L   `   `   `  ����E  ABubblyChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:BubblyChat.MVVM.ViewBubblyChat.MVVM.View  5   	   E'clr-namespace:BubblyChat.MVVM.ViewModelBubblyChat.MVVM.ViewModel  5   	   E'clr-namespace:BubblyChat.CustomControlsBubblyChat.CustomControls  6<   $    BubblyChat.MVVM.View.LoginView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:BubblyChat.MVVM.View     5   	   m iMahApps.Metro.IconPacks.MaterialDesign, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.PixelartIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Coolicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af	 bMahApps.Metro.IconPacks.Unicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai
 eMahApps.Metro.IconPacks.PicolIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.BoxIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.VaadinIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.EvaIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Octicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai eMahApps.Metro.IconPacks.RadixIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Codicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak gMahApps.Metro.IconPacks.FeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.MemoryIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.JamIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.CircumIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.MaterialLight, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai eMahApps.Metro.IconPacks.RPGAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Zondicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Ionicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.ForkAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Entypo, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.BootstrapIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Fontisto, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.PhosphorIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af bMahApps.Metro.IconPacks.Microns, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah  dMahApps.Metro.IconPacks.GameIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah! dMahApps.Metro.IconPacks.Fontaudio, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah" dMahApps.Metro.IconPacks.FileIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak# gMahApps.Metro.IconPacks.WeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a^$ ZMahApps.Metro.IconPacks, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj% fMahApps.Metro.IconPacks.SimpleIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae& aMahApps.Metro.IconPacks.Modern, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj' fMahApps.Metro.IconPacks.FontAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah( dMahApps.Metro.IconPacks.RemixIcon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag) cMahApps.Metro.IconPacks.Typicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae* aMahApps.Metro.IconPacks.Lucide, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a�icon-http://metro.mahapps.com/winfx/xaml/iconpacks&     	 
                        ! " # $ % & ' ( ) * 5   	   zvm'clr-namespace:BubblyChat.MVVM.ViewModel&     	 
                        ! " # $ % & ' ( ) * 5   	   �customcontrols'clr-namespace:BubblyChat.CustomControls&     	 
                        ! " # $ % & ' ( ) * 6<   -   5          � Title$  	LoginView��5
   	   $	��600��5   	   $	��450��5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency .  � DataContext 5      '   !BubblyChat.MVVM.ViewModel.LoginVM  5   
    � 	Resources 5         BooleanToVisibility��%   &	        �� 5   
   6'   5      y�5      �� 5    
    �� Path$ IsViewVisible�6    �� Mode$ TwoWay=�6'   	 �� 	Converter#	 [  65   .��5"   
   � .+�5&   
   �� 
 �� CornerRadius$	
 30 ��5'      ��5(      �� 5)       �� ImageSource$ /Images/Bubble.jpg��6   ._�5,      �� $��2��6   $	
 30 ��5-      $��3q�5.       J� Opacity$
 0.85Q�5/      ��50      �� 51       �� 
StartPoint$	 0,0&�6*    �� EndPoint$	 1 1&�52   &   .��53      �  � Color$ #CBAAFF��6'    � Offset$ 0Q�67   � 54      $ #B98AF3��6'   $
 0.75Q�67   � 55      $ #8A16C1��6'   $ 1Q�67   56      ��59      �� 5:      $	 0,0&�6*   $	 1 1&�5;   &   .��5<      � $ #060531��6'   $ 0Q�67   � 5=      $ #1B1448��6'   $	 0.5Q�67   � 5>      $ #FF4B6E��6'   $ 1Q�67   5?      5B      5C      � 5G   
    � RowDefinitions 5H      �� 5I      $��30 �6    �� 5J      $	��180 �6    �� 5K      $��* �6    .+�5O      �� $��1��6   $��Welcome to BubblyChat!��5P      $��30(�5Q      $
��Bold"�5R      
�������$��Center=�5T      $��10q�5U      �� 5V      $��1��6   $��Sign in��5W      $��30(�5X      $
��Bold"�5Y      
�������$��Center=�5[      $��Center=�5\      $��10q�5]      � 5_      .+�5`      �� $��Right=�6    �� Orientation$ 
Horizontal=�5a      $��0 3 18 0q�5b      .+�5c      �� -   5         ��Name$ btnMinimize��5c      $��0q�5d      $��25��5e      $��25��5f      $��0 3 0 0q�5g       � Cursor$
 Hand��5h      ��5m      �� 5n       �� 
TargetType" ��6$   .�5o   "   ��  �� Property � �� �� Value �#28AEED5p   )    �� Triggers 5q   "   P� 5r   &    P� Property �@J�IsMouseOver P� Value . . �5s   *   ��  � �� �	#FF11C2EF6G   5t   '   5v      ��5x      �� 5y       �� 
TargetType" ��6.   .R�5z   "   �� $��18��6)   $��18��64   $
 9��5{   )   #��z��5|   )   ._�5}   &   �� $��Center=�67   $��Center=�5~   7   5�   #   5�      .��5�      .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ WindowMinimize=�60   
�������$��10��5�   0   $��10��5�   0   $��Center=�5�   0   $��Center=�5�   0   5�      �� 5�      -   5        $ btnClose��5�      $��0q�5�      $��25��5�      $��25��5�      $��0 3 0 0q�5�      $
 Hand��5�      ��5�      �� 5�      " ��6$   .�5�   "   ��  � �� �	#FFF544285�   )    5�   "   P� 5�   &    �@J�IsMouseOver . . �5�   *   ��  � �� �	#FFF994006G   5�   '   5�      ��5�      �� 5�      " ��6.   .R�5�   "   �� $��18��6)   $��18��64   $
 0 9 0 0��5�   )   #��z��5�   )   ._�5�   &   �� $��Center=�67   $��Center=�5�   7   5�   #   5�      .��5�        $ WindowClose=�60   
�������$��10��5�   0   $��10��5�   0   $��Center=�5�   0   $��Center=�5�   0   5�      5�      5�      � 5�      $��2��6   $��10 0 0 0q�5�      .+�5�      �� $��1��6   $��0 20 0 0q�5�      .+�5�      �� $��Email��6    $��18(�5�       $��Medium"�5�       
�������$��Center=�5�        �� Padding$ 60 0 0 0q�5�       �� 5�      $ 
Horizontal=�6!   .+�5�      �� -   5         ��Name$ txtUser��5�      $��Light"�5�      $��20(�5�      $��0q�5�      $
��Left=�5�      
�������
������ $
 0.75Q�5�      $��25��5�      $	��300��5�      $��Center=�5�      $��60 0 0 0q�5�       �� 
CaretBrush
 �������5�      �� *
Username+  �� UpdateSourceTrigger$  PropertyChanged=�5�        5�      $ Email=�6,   $��Center=�5�   ,   $��Center=�5�   ,   
�������5�   ,   5�      �� 5�      $	��315��6   $��2��5�      
�������$	 0.5Q�5�      �� 5�      $��Password��6    $��	0 20 0 0 q�5�       $��18(�5�       $��Medium"�5�       
�������$��Center=�5�       $ 60 0 0 0q�5�       �� 5�      $ 
Horizontal=�6!   .+�5�      3   -BubblyChat.CustomControls.BindablePasswordBox  $��60 0 0 0q�5�   =   !   Password! 5�   =   �� *
Password+$ TwoWay=�$  PropertyChanged=�5�        5�      $	 Key=�60   $��Center=�5�   0   $��Center=�5�   0   
�������5�   0   5�      �� 5�      $	��315��6   $��2��5�      
�������$	 0.5Q�5�      �� 5      ���#D75960$��0 5 0 0 q�5      $��12(�5      $��Medium"�5      $��Center=�5      $ 60 0 0 0q�5      $
��Wrap=�5      ��5       �� *ErrorMessage+5      �� 5	     -   5        $ btnLogin��5	     $	��150��5     $��40��5     $��Login��5     
�������$
��Bold"�5     $
 Hand��5       BtnWindowManager#��[ 5     $��0 20 0 0q�5     ��5
     �� *LoginCommand+5     �� 5     $��0 10 0 0q�6!   $ Vertical=�5  !   .+�5     �� -   5        $ 	btnSignup��5  !   $��Sign up��5  !   $
 Hand��5  !   $��Light"�5  !   
������ ��5  !   �� *SignUpCommand+��5  !   �� " �� RelativeSource" �� # �� AncestorType$# WindowL���5     �� 5  "   " ��6(   .�5  &   ��  � ��
 ���� 6D   �� 5  &    � ��
 �����6C    5   &   P� 5!  *    �@J�IsMouseOver . . �5"  .   ��  � ��
 ��� �6K   5#  +   5%  #   ��5'     �� 5(  "   " ��62   .R�5)  &   �� $	��130��6-   $��20��69   #��z��5*  )   ._�5+  *   �� $��Center=�6;   $��Center=�5,  7   5.  '   5/  #   51     �� 52     -   5        $ btnResetPassword��52  !   $��Forgot PassWord ?��53  !   $
 Hand��54  !   $��Light"�55  !   
������ ��57     �� 58  "   " ��6(   .�59  &   ��  � ��
 ���� 6D   �� 5:  &    � ��
 �����6C    5;  &   P� 5<  *    �@J�IsMouseOver . . �5=  .   ��  � ��
 ��� �6K   5>  +   5@  #   ��5B     �� 5C  "   " ��62   .R�5D  &   �� $	��130��6-   $��20��69   #��z��5E  )   ._�5F  *   �� $��Center=�6;   $��Center=�5G  7   5I  '   5J  #   5L     5N     5O     5T     5U     5W     5X     