 htmlTable.Append("<tr><td colspan=\"" + 5 + "\">" + "COST ESTIMATE SUMMARY (U.S. $)" + "</td></tr>");
                htmlTable.Append("<tr><td colspan=\"" + 4 + "\">" + "Project :" + "&nbsp" + DT.Rows[0]["ProjectTitle"] + "</td><td>B.I No :" + "&nbsp" + DT.Rows[0]["ReqID"] + "</td></tr>");
                htmlTable.Append("<tr bgcolor=\"#AAAAAA\"><td></td></tr>");
                htmlTable.Append("<tr><td>Estimate No  :" + "&nbsp" + DT.Rows[0]["EstimateNo"] + "</td><td>Contract No :" + "&nbsp" + DT.Rows[0]["ContractNo"] + "</td><td>Request No :" + "&nbsp" + DT.Rows[0]["TxnNo"] + "</td><td>Rev. No :" + "&nbsp" + DT.Rows[0]["RevisionNo"] + "</td><td>Prepared By :" + "&nbsp" + User.Identity.Name + "</td></tr>");
                htmlTable.Append("<tr><td>Function :" + "&nbsp" + DT.Rows[0]["WFunction"] + "</td><td>Contract Strategy :" + "&nbsp" + DT.Rows[0]["ContractStrategy"] + "</td><td colspan=\"" + 2 + "\">" + "Estimated Class :" + "&nbsp" + "</td><td>Scope Agreed By :</td></tr>");
                htmlTable.Append("<tr><td rowspan=\"" + 1 + "\">C.R.D" + DT.Rows[0]["CRD"] + "</td><td rowspan=\"" + 2 + "\">CRD Exchange Rates</td><td></td><td></td><td bgcolor=\"#CC9966\">Ref :</td></tr>");
                htmlTable.Append("<tr><td rowspan=\"" + 1 + "\">Date Made :" + DT.Rows[0]["CRD"] + "</td><td></td><td></td><td bgcolor=\"#AAAAAA\"></td></tr>");
              