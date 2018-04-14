delete from Rides;
delete from Taxis;
delete from Drivers;

insert into Taxis (LicensePlate) values ('LL-123AB');
insert into Taxis (LicensePlate) values ('L-876XY');
insert into Taxis (LicensePlate) values ('S-235VW');
insert into Taxis (LicensePlate) values ('W-84775X');

insert into Drivers (Name) values ('Max Muster');
insert into Drivers (Name) values ('Veronica Maier');
insert into Drivers (Name) values ('Tim Turbo');
insert into Drivers (Name) values ('Eva Schnell');

insert into Rides (TaxiID, DriverID, [Start], [End], Charge)
select t.ID, d.ID, getdate(), null, null
    from Taxis t cross join Drivers d
    where t.LicensePlate = 'LL-123AB' and d.Name = 'Max Muster';
insert into Rides (TaxiID, DriverID, [Start], [End], Charge)
select t.ID, d.ID, getdate(), null, null
    from Taxis t cross join Drivers d
    where t.LicensePlate = 'L-876XY' and d.Name = 'Max Muster';
insert into Rides (TaxiID, DriverID, [Start], [End], Charge)
select t.ID, d.ID, getdate(), getdate(), 99
    from Taxis t cross join Drivers d
    where t.LicensePlate = 'S-235VW' and d.Name = 'Eva Schnell';
insert into Rides (TaxiID, DriverID, [Start], [End], Charge)
select t.ID, d.ID, getdate(), getdate(), 50
    from Taxis t cross join Drivers d
    where t.LicensePlate = 'S-235VW' and d.Name = 'Tim Turbo';
insert into Rides (TaxiID, DriverID, [Start], [End], Charge)
select t.ID, d.ID, getdate(), getdate(), 50
    from Taxis t cross join Drivers d
    where t.LicensePlate = 'W-84775X' and d.Name = 'Veronica Maier';
