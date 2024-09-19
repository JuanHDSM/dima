select `tran`.`userId
` AS `userId`,month
(`tran`.`paidOrReceivedAt`) AS `month`,year
(`tran`.`paidOrReceivedAt`) AS `year`,sum
(case when `tran`.`type` = 1 then `tran`.`amount` else 0
end) AS `incomes`,sum
(case when `tran`.`type` = 2 then `tran`.`amount` else 0
end) AS `expenses` from `transaction` `tran` where `tran`.`paidOrReceivedAt` >= curdate
() + interval -11 month and `tran`.`paidOrReceivedAt` <= curdate
() + interval 1 month group by `tran`.`userId`,month
(`tran`.`paidOrReceivedAt`),year
(`tran`.`paidOrReceivedAt`)