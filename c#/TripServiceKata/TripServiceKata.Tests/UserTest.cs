using NUnit.Framework;
namespace TripServiceKata.Tests
{
    using User = User.User;
    [TestFixture]
    public class UserTests
    {
        private User _user;

        [SetUp]
        public void SetUp()
        {
            _user = new User();
        }

        [Test]
        public void AddFriend_ShouldAddUserToFriendsList()
        {
            var friend = new User();
            _user.AddFriend(friend);

            Assert.Contains(friend, _user.GetFriends());
        }

        [Test]
        public void AddTrip_ShouldAddTripToTripsList()
        {
            var trip = new Trip.Trip();
            _user.AddTrip(trip);

            Assert.Contains(trip, _user.Trips());
        }

        [Test]
        public void GetFriends_ShouldReturnListOfFriends()
        {
            var friend1 = new User();
            var friend2 = new User();

            _user.AddFriend(friend1);
            _user.AddFriend(friend2);

            var friends = _user.GetFriends();

            Assert.AreEqual(2, friends.Count);
            Assert.Contains(friend1, friends);
            Assert.Contains(friend2, friends);
        }

        [Test]
        public void IsFriend_ShouldReturnTrueIfUserIsFriend()
        {
            var friend = new User();
            friend.AddFriend(_user);

            Assert.IsTrue(_user.IsFriend(friend));
        }

        [Test]
        public void IsFriend_ShouldReturnFalseIfUserIsNotFriend()
        {
            var notFriend = new User();

            Assert.IsFalse(_user.IsFriend(notFriend));
        }
    }
}