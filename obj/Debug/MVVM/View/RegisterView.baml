   M S B A M L   `   `   `  ����E  ABubblyChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:BubblyChat.MVVM.ViewBubblyChat.MVVM.View  5   	   E'clr-namespace:BubblyChat.CustomControlsBubblyChat.CustomControls  5   	   E'clr-namespace:BubblyChat.MVVM.ViewModelBubblyChat.MVVM.ViewModel  5	   	   '    !BubblyChat.MVVM.View.RegisterView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:BubblyChat.MVVM.View     5   	   m iMahApps.Metro.IconPacks.MaterialDesign, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.PixelartIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Coolicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af	 bMahApps.Metro.IconPacks.Unicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai
 eMahApps.Metro.IconPacks.PicolIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.BoxIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.VaadinIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.EvaIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Octicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai eMahApps.Metro.IconPacks.RadixIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Codicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak gMahApps.Metro.IconPacks.FeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.MemoryIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.JamIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.CircumIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.MaterialLight, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai eMahApps.Metro.IconPacks.RPGAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Zondicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Ionicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.ForkAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Entypo, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.BootstrapIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Fontisto, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.PhosphorIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af bMahApps.Metro.IconPacks.Microns, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah  dMahApps.Metro.IconPacks.GameIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah! dMahApps.Metro.IconPacks.Fontaudio, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah" dMahApps.Metro.IconPacks.FileIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak# gMahApps.Metro.IconPacks.WeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a^$ ZMahApps.Metro.IconPacks, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj% fMahApps.Metro.IconPacks.SimpleIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae& aMahApps.Metro.IconPacks.Modern, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj' fMahApps.Metro.IconPacks.FontAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah( dMahApps.Metro.IconPacks.RemixIcon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag) cMahApps.Metro.IconPacks.Typicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae* aMahApps.Metro.IconPacks.Lucide, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a�icon-http://metro.mahapps.com/winfx/xaml/iconpacks&     	 
                        ! " # $ % & ' ( ) * 5   	   �customcontrols'clr-namespace:BubblyChat.CustomControls&     	 
                        ! " # $ % & ' ( ) * 5   	   zvm'clr-namespace:BubblyChat.MVVM.ViewModel&     	 
                        ! " # $ % & ' ( ) * 5	   	   -   5          � Title$  RegisterView��5   	   $	��600��5   	   $	��500��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	   
������  � AllowsTransparency .  � DataContext 5      *   $BubblyChat.MVVM.ViewModel.RegisterVM  5   
    � 	Resources 5         BooleanToVisibility��%   &	        �� 5   
   6'   5      y�5      �� 5   
    �� Path$ IsViewVisible�6    �� Mode$ TwoWay=�6'   	 �� 	Converter#	 [  65   .��5!   
   � .+�5#   
   �� 
 �� CornerRadius$	
 30 ��5$   
   ��5%      �� 5&       �� ImageSource$ /Images/Bubble.jpg��6   ._�5)      �� $��2��6   $	
 30 ��5*      $��5q�5+       J� Opacity$
 0.85Q�5,      ��5-      �� 5.       �� 
StartPoint$	 0,0&�6*    �� EndPoint$	 1 1&�5/   *   .��50      �  � Color$ #CBAAFF��6'    � Offset$ 0Q�67   � 51      $ #B98AF3��6'   $
 0.75Q�67   � 52      $ #8A16C1��6'   $ 1Q�67   53      ��56      �� 57      $	 0,0&�6*   $	 1 1&�58   *   .��59      � $ #060531��6'   $ 0Q�67   � 5:      $ #1B1448��6'   $	 0.5Q�67   � 5;      $ #FF4B6E��6'   $ 1Q�67   5<      5?      5@      � 5B   
    � RowDefinitions 5C      �� 5D      $��30 �6    �� 5E      $	��150 �6    �� 5F      $��* �6    .+�5K      �� $��Right=�6    �� Orientation$ 
Horizontal=�5L      $��0 3 15 0q�5M      .+�5N      �� -   5         ��Name$ btnMinimize��5N      $��0q�5O      $��25��5P      $��25��5Q      $��0 3 0 0q�5R       � Cursor$
 Hand��5S      ��5X      �� 5Y       �� 
TargetType" ��6    .�5Z      ��  �� Property � �� �� Value �#28AEED5[   %    �� Triggers 5\      P� 5]   "    P� Property �@J�IsMouseOver P� Value . . �5^   &   ��  � �� �	#FF11C2EF6C   5_   #   5a      ��5c      �� 5d       �� 
TargetType" ��6*   .R�5e      �� $��18��6%   $��18��60   $
 9��5f   %   #��z��5g   %   ._�5h   "   �� $��Center=�63   $��Center=�5i   3   5k      5l      .��5n      .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ WindowMinimize=�6,   
�������$��10��5p   ,   $��10��5q   ,   $��Center=�5r   ,   $��Center=�5s   ,   5t      �� 5v      -   5        $ btnClose��5v      $��0q�5w      $��25��5x      $��25��5y      $��0 3 0 0q�5z      $
 Hand��5{      ��5~      �� 5      " ��6    .�5�      ��  � �� �	#FFF544285�   %    5�      P� 5�   "    �@J�IsMouseOver . . �5�   &   ��  � �� �	#FFF994006C   5�   #   5�      ��5�      �� 5�      " ��6*   .R�5�      �� $��18��6%   $��18��60   $
 9��5�   %   #��z��5�   %   ._�5�   "   �� $��Center=�63   $��Center=�5�   3   5�      5�      .��5�        $ WindowClose=�6,   
�������$��10��5�   ,   $��10��5�   ,   $��Center=�5�   ,   $��Center=�5�   ,   5�      5�      �� 5�      $��1��6   $��Welcome to BubblyChat!��5�      $��30(�5�      $
��Bold"�5�      
�������$��Center=�5�      $��10q�5�      �� 5�      $��1��6   $��Sign up��5�      $��30(�5�      $
��Bold"�5�      
�������$��Center=�5�      $��Center=�5�      $��10 30 10 10q�5�      � 5�      $��2��6   $��10 0 0 0q�5�      .+�5�      �� $��1��6   $��0 20 0 0q�5�      .+�5�      �� $��Email��6    $��18(�5�       $��Medium"�5�       
�������$��Center=�5�        �� Padding$ 85 0 0 0q�5�       �� 5�      $ 
Horizontal=�6!   .+�5�      �� $��Light"�6"   $��20(�5�   "   $��0q�5�   "   $
��Left=�5�   "   
�������
������ $
 0.75Q�5�   "   $��25��5�   "   $	��300��5�   "   $��Center=�5�   "   $��85 0 0 0q�5�   "    �� 
CaretBrush
 �����5�   "     5�      $ Account=�60   $��Center=�5�   ,   $��Center=�5�   ,   
�������5�   ,   5�      �� 5�      $	��315��6   $��2��5�      
�������$	 0.5Q�5�      �� 5�      $��Password��6    $��	0 20 0 0 q�5�       $��18(�5�       $��Medium"�5�       
�������$��Center=�5�       $ 85 0 0 0q�5�       �� 5�      $ 
Horizontal=�6!   .+�5�      3   -BubblyChat.CustomControls.BindablePasswordBox  $��85 0 0 0q�6=      Password 5�   =   �� *
Password+$ TwoWay=�  �� UpdateSourceTrigger$  PropertyChanged=�5�        5�      $	 Key=�60   $��Center=�5�   ,   $��Center=�5�   ,   
�������5�   ,   5�      �� 5�      $	��315��6   $��2��5�      
�������$	 0.5Q�5�      �� 5�      $��Confirm Password��6   $��	0 20 0 0 q�5�      $��18(�5�      $��Medium"�5�      
�������$��Center=�5�      $��85 0 0 0q�5�      �� 5�      $ 
Horizontal=�6!   .+�5�        $��85 0 0 0q�6=    5�   =   �� *ConfirmPassword+$ TwoWay=�$  PropertyChanged=�5       5     $	 Key=�60   $��Center=�5  ,   $��Center=�5  ,   
�������5  ,   5	     �� 5
     $	��315��6   $��2��5     
�������$	 0.5Q�5     �� 5*     -   5        $ btnRegister��5*     $	��150��5+     $��40��5,     $��OK��5-     
�������$
��Bold"�5/     $
 Hand��50       BtnWindowManager#��[ 51     $��0 20 0 0q�52     �� 53     $��0 10 0 0q�6!   $ Vertical=�54  !   .+�55     �� -   5        $ 	btnSignIn��55  !   $	��130��58  !   $��20��59  !   $��I already have account��5:  !   $
 Hand��5;  !   $��Light"�5<  !   
������ ��56  !   �� *SignInCommand+��57  !   �� ! �� RelativeSource! �� " �� AncestorType$" WindowL���5>     �� 5?  "   " ��6(   .�5@  &   ��  � ��
 ���� 6D   �� 5A  &    � ��
 �����6C    5B  &   P� 5C  *    �@J�IsMouseOver . . �5D  .   ��  � ��
 ��� �6K   5E  +   5G  #   ��5I     �� 5J  "   " ��62   .R�5K  &   �� $	��130��6-   $��20��69   #��z��5L  )   ._�5M  *   �� $��Center=�6;   $��Center=�5N  7   5P  '   5Q  #   5S     5T     5U     5Z     5\     5^     5_     