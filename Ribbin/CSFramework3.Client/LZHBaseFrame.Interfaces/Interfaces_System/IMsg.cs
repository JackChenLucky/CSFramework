///*************************************************************************/
///*
///* �ļ���    ��IMsg.cs                                
///* ����˵��  : ��Ϣ��ʾ�ӿ�
///* ԭ������  �������� 
///* 
///* Copyright 2010-2011 C/S����� www.LZHBaseFrame.com
///*
///**************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace LZHBaseFrame.Core
{
    /// <summary>
    /// ��Ϣ��ʾ�ӿ�
    /// </summary>
    public interface IMsg
    {
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="msg"></param>
        void UpdateMessage(string msg,int step);
    }
}
