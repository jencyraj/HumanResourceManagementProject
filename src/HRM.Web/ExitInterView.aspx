<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ExitInterView.aspx.cs" Inherits="ExitInterView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section class="content-header">
        <h1>
            <%= hrmlang.GetString("exitinterview") %><small></small></h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>
                <%= hrmlang.GetString("home") %></a></li>
            <li class="active">
                <%= hrmlang.GetString("mexitinterview")%></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="col-mg-12 rowmargin">
                    <b>
                        <%= hrmlang.GetString("resignations") %></b>
                    <asp:GridView ID="gvResignation" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvResignation_RowCommand" DataKeyNames="Resgnid" EnableViewState="True"
                        OnRowDataBound="gvResignation_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="FirstName" HeaderText="EmployeeName" />
                            <asp:BoundField DataField="NoticeDate" HeaderText="Notice Date" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="ResgnDate" HeaderText="Resignation Date" DataFormatString="{0:d}" />
                           <asp:BoundField DataField="InterviewDate" HeaderText="Interview Date" DataFormatString="{0:d}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkInterview" runat="server" data-toggle="tooltip" CssClass="fa fa-thumbs-up"
                                        NavigateUrl='<%# String.Concat("ExitInterviewResult.aspx?exittype=reg&id=",Eval("Resgnid")) %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                        </Columns>
                    </asp:GridView>
                    <b>
                        <%= hrmlang.GetString("terminations") %></b>
                    <asp:GridView ID="gvTermination" runat="Server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped dataTable"
                        OnRowCommand="gvTermination_RowCommand" DataKeyNames="TID" EnableViewState="True"
                        OnRowDataBound="gvTermination_RowDataBound">
                        <PagerStyle HorizontalAlign="Right" />
                        <Columns>
                            <asp:BoundField DataField="FirstName" HeaderText="EmployeeName" />
                              <asp:BoundField DataField="Iname" HeaderText="ForwardTo" />
                           
                            <asp:BoundField DataField="RequestDate" HeaderText="Request Date" />
                            <asp:BoundField DataField="InterviewDate" HeaderText="Interview Date" DataFormatString="{0:d}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkInterview" runat="server" data-toggle="tooltip" CssClass="fa fa-thumbs-up"
                                        NavigateUrl='<%# String.Concat("ExitInterviewResult.aspx?exittype=term&id=",Eval("TID")) %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
