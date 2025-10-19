-- Seed data for PropertyManagement database
-- Note: No test data for Users entity as per requirements

-- Insert sample Hosts
INSERT INTO Hosts (FullName, Email, Phone) VALUES
('John Doe', 'john.doe@example.com', '+1-555-0101'),
('Jane Smith', 'jane.smith@example.com', '+1-555-0102'),
('Bob Johnson', 'bob.johnson@example.com', '+1-555-0103'),
('Alice Brown', 'alice.brown@example.com', '+1-555-0104'),
('Charlie Wilson', 'charlie.wilson@example.com', '+1-555-0105');

-- Insert sample Properties
INSERT INTO Properties (HostId, Name, Description, Location, PricePerNight, Status, CreatedAt, UpdatedAt) VALUES
(1, 'Cozy Apartment Downtown', 'A comfortable 2-bedroom apartment in the heart of the city with modern amenities.', '123 Main St, Downtown, City', 120.00, 'Available', '2024-01-15 10:00:00', '2024-01-15 10:00:00'),
(1, 'Luxury Penthouse', 'Stunning penthouse with panoramic city views and premium furnishings.', '456 High St, Uptown, City', 350.00, 'Available', '2024-01-20 14:30:00', '2024-01-20 14:30:00'),
(2, 'Beachfront Villa', 'Beautiful villa right on the beach with private pool and ocean views.', '789 Ocean Ave, Beach District, City', 280.00, 'Available', '2024-02-01 09:15:00', '2024-02-01 09:15:00'),
(3, 'Mountain Cabin', 'Rustic cabin in the mountains, perfect for a peaceful getaway.', '321 Mountain Rd, Forest Area, City', 95.00, 'Available', '2024-02-10 16:45:00', '2024-02-10 16:45:00'),
(4, 'Urban Loft', 'Modern loft in the trendy arts district with exposed brick walls.', '654 Art St, Arts District, City', 150.00, 'Available', '2024-02-15 11:20:00', '2024-02-15 11:20:00'),
(5, 'Family Home', 'Spacious 4-bedroom home ideal for families, with backyard and garage.', '987 Family Ln, Suburban Area, City', 200.00, 'Available', '2024-03-01 13:00:00', '2024-03-01 13:00:00');

-- Insert sample Bookings
INSERT INTO Bookings (PropertyId, CheckIn, CheckOut, TotalPrice) VALUES
(1, '2024-04-01', '2024-04-05', 480.00),
(2, '2024-04-10', '2024-04-15', 1750.00),
(3, '2024-04-20', '2024-04-25', 1400.00),
(4, '2024-05-01', '2024-05-03', 190.00),
(5, '2024-05-10', '2024-05-12', 300.00),
(6, '2024-05-15', '2024-05-20', 1000.00);

-- Insert sample DomainEvents
INSERT INTO DomainEvents (PropertyId, EventType, CreatedAt, PayloadJSON) VALUES
(1, 'PropertyCreated', '2024-01-15 10:00:00', '{"action": "created", "propertyName": "Cozy Apartment Downtown"}'),
(1, 'PriceUpdated', '2024-01-16 12:00:00', '{"oldPrice": 100.00, "newPrice": 120.00}'),
(2, 'PropertyCreated', '2024-01-20 14:30:00', '{"action": "created", "propertyName": "Luxury Penthouse"}'),
(3, 'PropertyCreated', '2024-02-01 09:15:00', '{"action": "created", "propertyName": "Beachfront Villa"}'),
(4, 'PropertyCreated', '2024-02-10 16:45:00', '{"action": "created", "propertyName": "Mountain Cabin"}'),
(5, 'PropertyCreated', '2024-02-15 11:20:00', '{"action": "created", "propertyName": "Urban Loft"}'),
(6, 'PropertyCreated', '2024-03-01 13:00:00', '{"action": "created", "propertyName": "Family Home"}'),
(1, 'BookingCreated', '2024-03-15 08:30:00', '{"bookingId": 1, "checkIn": "2024-04-01", "checkOut": "2024-04-05"}'),
(2, 'BookingCreated', '2024-03-20 10:15:00', '{"bookingId": 2, "checkIn": "2024-04-10", "checkOut": "2024-04-15"}');
