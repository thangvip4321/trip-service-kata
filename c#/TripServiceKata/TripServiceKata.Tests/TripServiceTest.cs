using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using TripServiceKata.User;
namespace TripServiceKata.Tests
{

    public class TripServiceTest
    {

        private static readonly User.User GUEST = null;
        private static readonly User.User ANY_USER = new User.User();
        private static readonly User.User REGISTERED_USER = new User.User();
        private TripDAO mockDao;
        private TripService tripService;
        [SetUp]
        public void SetUp()
        {
            mockDao = Substitute.For<TripDAO>();
            tripService = new TripService(mockDao);
        }
        [Test]
        public void TripService_WhenUserIsNotLoggedIn_ShouldThrowAnException()
        {
            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(ANY_USER,GUEST));
            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(null,GUEST));
        }
        [Test]
        public void TripService_Return_No_Trip_If_User_Are_Not_Friend()
        {
            var stranger = new User.User();
            stranger.AddTrip(new Trip.Trip());
            var trips = tripService.GetTripsByUser(stranger,REGISTERED_USER);
            Assert.IsEmpty(trips);
        }

        [Test]
        public void TripService_Return_Trip_If_User_Are_Friend()
        {
            var friend = new User.User();
            friend.AddFriend(REGISTERED_USER);
            friend.AddTrip(new Trip.Trip());
            mockDao.tripsBy(friend).Returns(friend.Trips());
            var trips = tripService.GetTripsByUser(friend,REGISTERED_USER);
            Assert.AreEqual(1, trips.Count);
        }
    }
}