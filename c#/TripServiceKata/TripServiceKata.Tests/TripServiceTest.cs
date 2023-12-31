using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using TripServiceKata.User;
namespace TripServiceKata.Tests
{
    public class TestableTripService : TripService
    {
        private User.User loggedInUser = null;
        protected override User.User GetLoggedUser()
        {
            return loggedInUser;
        }
        public TestableTripService(TripDAO dao, User.User loggedInUser) :base(dao){
            this.loggedInUser = loggedInUser;
        }
    }
    public class TripServiceTest
    {
        // public User.User LOGGED_IN_USER = new User.User();
        private static readonly User.User GUEST = null;
        private static readonly User.User ANY_USER = new User.User();
        private TripDAO mockDao;
        [SetUp]
        public void SetUp()
        {
            mockDao = Substitute.For<TripDAO>();
        }
        [Test]
        public void TripService_WhenUserIsNotLoggedIn_ShouldThrowAnException()
        {
            User.User GUEST = null;
            var service = new TestableTripService(mockDao,GUEST);
            Assert.Throws<UserNotLoggedInException>(() => service.GetTripsByUser(null));
            Assert.Throws<UserNotLoggedInException>(() => service.GetTripsByUser(ANY_USER));
        }
        [Test]
        public void TripService_Return_No_Trip_If_User_Are_Not_Friend()
        {
            var loggedInUser = new User.User();
            var service = new TestableTripService(mockDao,loggedInUser);
            var stranger = new User.User();
            stranger.AddTrip(new Trip.Trip());
            var trips = service.GetTripsByUser(stranger);
            Assert.IsEmpty(trips);
        }

        [Test]
        public void TripService_Return_Trip_If_User_Are_Friend(){
            var loggedInUser = new User.User();
            var friend = new User.User();
            friend.AddFriend(loggedInUser);
            friend.AddTrip(new Trip.Trip());
            var service = new TestableTripService(mockDao,loggedInUser);
            mockDao.FindTripsByUser(friend).Returns(friend.Trips());
            
            var trips = service.GetTripsByUser(friend);
            Assert.AreEqual(1,trips.Count);
        }
    }
}