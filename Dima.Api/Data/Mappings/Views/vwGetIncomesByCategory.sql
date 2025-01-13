CREATE OR REPLACE VIEW vwGetIncomesByCategory AS
SELECT
    tran.userId AS userId,
    cat.title AS category,
    YEAR(tran.paidOrReceivedAt) AS year,
    SUM(tran.amount) AS incomes
FROM
    `transaction` tran
    JOIN `category` cat ON cat.id = tran.categoryId
WHERE
    tran.paidOrReceivedAt >= CURDATE() - INTERVAL 11 MONTH
  AND tran.paidOrReceivedAt <= CURDATE() + INTERVAL 1 MONTH
  AND tran.type = 1
GROUP BY
    tran.userId,
    cat.title,
    YEAR(tran.paidOrReceivedAt);
