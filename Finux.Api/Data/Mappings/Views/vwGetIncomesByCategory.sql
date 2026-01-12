select `tran`.`userId
` AS `userId`,`cat`.`title` AS `category`,year
(`tran`.`paidOrReceivedAt`) AS `year`,sum
(`tran`.`amount`) AS `expenses` from
(`transaction` `tran` join `category` `cat` on
(`cat`.`id` = `tran`.`categoryId`)) where `tran`.`paidOrReceivedAt` >= curdate
() + interval -11 month and `tran`.`paidOrReceivedAt` <= curdate
() + interval 1 month and `tran`.`type` = 1 group by `tran`.`userId`,`cat`.`title`,year
(`tran`.`paidOrReceivedAt`)