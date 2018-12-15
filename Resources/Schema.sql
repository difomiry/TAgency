--
-- The database schema.
--

CREATE TABLE IF NOT EXISTS "Client"
(
	"ClientID" integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"Name" text NOT NULL,
	"Address" text NOT NULL,
	"Phone" text NOT NULL
);

CREATE TABLE IF NOT EXISTS "Hotel"
(
	"HotelID" integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"Name" text NOT NULL,
	"Country" text NOT NULL,
	"Climate" text NOT NULL,
	"Cost" real NOT NULL
);

CREATE TABLE IF NOT EXISTS "Discount"
(
	"DiscountID" integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"Name" text NOT NULL,
	"Percent" real NOT NULL
);

CREATE TABLE IF NOT EXISTS "Tour"
(
	"TourID" integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"Date" datetime NOT NULL
);

CREATE TABLE IF NOT EXISTS "ClientDiscount"
(
	"ClientID" integer NOT NULL,
	"DiscountID" integer NOT NULL,
	FOREIGN KEY("ClientID") REFERENCES "Client"("ClientID") ON DELETE CASCADE,
	FOREIGN KEY("DiscountID") REFERENCES "Discount"("DiscountID") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS "TourHotel"
(
	"TourID" integer NOT NULL,
	"HotelID" integer NOT NULL,
	"Duration" integer NOT NULL,
	FOREIGN KEY("TourID") REFERENCES "Tour"("TourID") ON DELETE CASCADE,
	FOREIGN KEY("HotelID") REFERENCES "Hotel"("HotelID") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS "Voucher"
(
	"VoucherID" integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"ClientID" integer NOT NULL,
	"TourID" integer NOT NULL,
	"Count" integer NOT NULL,
	"Cost" real NULL,
	FOREIGN KEY("ClientID") REFERENCES "Client"("ClientID") ON DELETE CASCADE,
	FOREIGN KEY("TourID") REFERENCES "Tour"("TourID") ON DELETE CASCADE
);

--
-- The ClientWithDiscount view.
--

CREATE VIEW IF NOT EXISTS "ClientWithDiscount" AS
SELECT "c"."ClientID", "Name", "Address", "Phone", "cc"."Discount"
FROM "Client" "c", (
	SELECT "Client"."ClientID", IFNULL(SUM("d"."Percent"), 0) "Discount"
	FROM "Client"
	LEFT JOIN "ClientDiscount" "cd" ON "cd"."ClientID" = "Client"."ClientID"
	LEFT JOIN "Discount" "d" ON "d"."DiscountID" = "cd"."DiscountID"
	GROUP BY "Client"."ClientID"
) "cc"
WHERE "c"."ClientID" = "cc"."ClientID";

--
-- The TourWithDurationAndCost view.
--

CREATE VIEW IF NOT EXISTS "TourWithDurationAndCost" AS
SELECT "t"."TourID", "t"."Date", "tt"."Cost"
FROM "Tour" "t", (
	SELECT "Tour"."TourID", IFNULL(SUM("th"."Duration"), 0) "Duration", IFNULL(SUM("th"."Duration" * "h"."Cost"), 0) "Cost"
	FROM "Tour"
	LEFT JOIN "TourHotel" "th" ON "th"."TourID" = "Tour"."TourID"
	LEFT JOIN "Hotel" "h" ON "h"."HotelID" = "th"."HotelID"
	GROUP BY "Tour"."TourID"
) "tt"
WHERE "t"."TourID" = "tt"."TourID";

--
-- The VoucherAfterInsert trigger.
--

CREATE TRIGGER IF NOT EXISTS "VoucherAfterInsert" AFTER INSERT ON "Voucher" BEGIN

	UPDATE "Voucher" SET "Cost" = (
		SELECT (1 - "cd"."Discount") * ("tdc"."Cost" * 1.3) * "vv"."Count"
		FROM "Voucher" "vv"
		INNER JOIN "ClientWithDiscount" "cd" ON "cd"."ClientID" = "vv"."ClientID"
		INNER JOIN "TourWithDurationAndCost" "tdc" ON "tdc"."TourID" = "vv"."TourID"
		WHERE "Voucher"."VoucherID" = "vv"."VoucherID"
	) WHERE "Cost" IS NULL;

END;