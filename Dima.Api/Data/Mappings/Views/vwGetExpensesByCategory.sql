CREATE OR REPLACE VIEW vwGetExpensesByCategory AS
SELECT
    tran.userId,
    cat.title AS category,
    YEAR(tran.paidOrReceivedAt) AS year,
    SUM(tran.amount) AS expenses
FROM
    `transaction` tran
    INNER JOIN
    `category ` cat
    ON 
        tran.categoryId = cat.id
WHERE
        tran.paidOrReceivedAt >= DATE_SUB(CURDATE(), INTERVAL
11 MONTH)
    AND 
        tran.paidOrReceivedAt < DATE_ADD
(CURDATE
(), INTERVAL 1 MONTH)
    AND 
        tran.type = 2
    GROUP BY
        tran.userId,
        cat.title,
        YEAR
(tran.paidOrReceivedAt);
