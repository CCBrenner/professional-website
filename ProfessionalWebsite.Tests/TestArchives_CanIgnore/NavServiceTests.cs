using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfessionalWebsite.Client.Services;

namespace ProfessionalWebsite.Tests
{
    /*
    The tests below were used before moving NavService to NavMgmt singleton, and then to NavMgmt class housed in UIService
    These are kept as artifacts of what NavService tests looked like when it existed; it has since been transformed
    This was an attempt at TDD which was flawed by a conflation with the concept of code coverage being the goal when
    the goal is actually desired system behavior and well-designed code (BDD) with a very valuable side-product of tests
    as checks on system behavior; existence of tests confirm well-written code because the code is first of all testable
    */

    /*
    [TestClass]
    public class NavServiceTests
    {
        [TestMethod]
        public void TestNavServiceConfiguration()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);
        }
        // start UpdateNav() tests
        [TestMethod]
        public void TestUpdateNavUserClicksHighlightedButtonWithPanelInvisible()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.UpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("panel-visible", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);
        }
        [TestMethod]
        public void TestUpdateNavUserClicksHighlightedButtonWithPanelVisible()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.UpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("panel-visible", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.UpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);
        }
        [TestMethod]
        public void TestUpdateNavUserClicksNonhighlightedButtonWithPanelVisible()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.UpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("panel-visible", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            // User clicks button of different panel
            NavService.UpdateNav(1);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("highlight-button", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("panel-visible", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);
        }
        [TestMethod]
        public void TestUpdateNavUserClicksNonhighlightedButtonWithPanelInvisible()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);


            NavService.UpdateNav(0);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("highlight-button", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("panel-visible", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);
        }
        // end UpdateNav() tests
        // start RouteUserAndUpdateNav() tests (originally start UpdateNavFromBehindPanel() tests)
        [TestMethod]
        public void TestRouteUserAndUpdateNavPanelIsVisibleAndLocationIsPanelsLocation()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.UpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("panel-visible", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.UpdateNavFromBehindPanel();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);
        }
        [TestMethod]
        public void TestRouteUserAndUpdateNavPanelIsVisibleAndLocationIsNOTPanelsLocation()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.UpdateNav(4);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("panel-visible", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);


            NavService.UpdateNavFromBehindPanel();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);
        }
        // continue RouteUserAndUpdateNav() tests (originally end UpdateNavFromBehindPanel() tests)
        // continue RouteUserAndUpdateNav() tests (originally start UpdateNavFromPanel() tests)
        [TestMethod]
        public void TestRouteUserAndUpdateNavUserClicksPanelOfCurrentLocationForCurrentLocation()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            // Since panel method, panel has to be accessible, which requires this to happen first:
            NavService.UpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("panel-visible", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.RouteUserAndUpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);
        }
        [TestMethod]
        public void TestRouteUserAndUpdateNavUserClicksPanelOfNonCurrentLocationForCurrentLocation()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.UpdateNav(3);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("highlight-button", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("panel-visible", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);


            NavService.RouteUserAndUpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);
        }
        [TestMethod]
        public void TestRouteUserAndUpdateNavUserClicksPanelOfCurrentLocationForNoncurrentLocation()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.UpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("panel-visible", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);


            NavService.RouteUserAndUpdateNav(0);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("highlight-button", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);
        }
        [TestMethod]
        public void TestRouteUserAndUpdateNavUserClicksPanelOfNonCurrentLocationForNoncurrentLocation()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.UpdateNav(4);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("highlight-button", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("panel-visible", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);


            NavService.RouteUserAndUpdateNav(1);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("highlight-button", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);
        }
        [TestMethod]
        public void TestRouteUserAndUpdateNavUserClicksPanelOfNonCurrentLocationForPanelLocation()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.UpdateNav(4);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("highlight-button", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("panel-visible", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);


            NavService.RouteUserAndUpdateNav(4);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("highlight-button", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[4].IsThisLocation);
        }
        // continue RouteUserAndUpdateNav() tests (originally end UpdateNavFromPanel() tests)
        // continue RouteUserAndUpdateNav() tests (originally start UpdateNavFromDrawer() tests)
        [TestMethod]
        public void TestRouteUserAndUpdateNavUserRoutedToSameSection()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.RouteUserAndUpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);

        }
        [TestMethod]
        public void TestRouteUserAndUpdateNavUserRoutedToDifferentSection()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.RouteUserAndUpdateNav(3);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("highlight-button", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);
        }
        // continue RouteUserAndUpdateNav() tests (originally end UpdateNavFromDrawer() tests)
        // continue RouteUserAndUpdateNav() tests (originally start UpdateNavFromAppTitle() tests)
        [TestMethod]
        public void TestRouteUserAndUpdateNavUserRoutedToCurrentLocation()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.RouteUserAndUpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);
        }
        [TestMethod]
        public void TestRouteUserAndUpdateNavUserRoutedToOtherLocationThanCurrentOne()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.AssociatedNav[2] = new AssociatedNavButtonAndPanel()
            {
                NavButtonStatus = "",
                NavPanelStatus = "",
                IsThisLocation = false,
            };
            NavService.AssociatedNav[0] = new AssociatedNavButtonAndPanel()
            {
                NavButtonStatus = "highlight-button",
                NavPanelStatus = "",
                IsThisLocation = true,
            };


            NavService.RouteUserAndUpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);


            NavService.AssociatedNav[2] = new AssociatedNavButtonAndPanel()
            {
                NavButtonStatus = "",
                NavPanelStatus = "",
                IsThisLocation = false,
            };
            NavService.AssociatedNav[3] = new AssociatedNavButtonAndPanel()
            {
                NavButtonStatus = "highlight-button",
                NavPanelStatus = "panel-visible",
                IsThisLocation = true,
            };


            NavService.RouteUserAndUpdateNav(2);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);
        }
        // end RouteUserAndUpdateNav() tests (originally end UpdateNavFromAppTitle() tests)

        // Animation Methods:

        // start original ShowLayoutControls() tests
        [TestMethod]
        public void TestShowLayoutControlsUpdatesNavServiceCorrectlyWhenCurrentLocationEqualsRoutedLocation() 
        {
            NavService NavService = new NavService();
            Assert.AreEqual("", NavService.LayoutControls);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.ShowLayoutControls(2);
            Assert.AreEqual("", NavService.BehindPanel);
            Assert.AreEqual("", NavService.ContentBlur);
            Assert.AreEqual("layout-controls-on", NavService.LayoutControls);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[4].IsThisLocation);
        }
        [TestMethod]
        public void TestShowLayoutControlsUpdatesNavServiceCorrectlyWhenRouteLocationIsNOTCurrentLocation()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("", NavService.LayoutControls);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.ShowLayoutControls(4);
            Assert.AreEqual("layout-controls-on", NavService.LayoutControls);
            Assert.AreEqual("", NavService.BehindPanel);
            Assert.AreEqual("", NavService.ContentBlur);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[2].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[0].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[0].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[0].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[1].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[1].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[1].IsThisLocation);

            Assert.AreEqual("", NavService.AssociatedNav[3].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[3].NavPanelStatus);
            Assert.AreEqual(false, NavService.AssociatedNav[3].IsThisLocation);

            Assert.AreEqual("highlight-button", NavService.AssociatedNav[4].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[4].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[4].IsThisLocation);
        }
        [TestMethod]
        public void TestShowLayoutControlsSelectNavServiceMethodsClearLayoutControlsProperty()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("", NavService.LayoutControls);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            NavService.ShowLayoutControls(4);
            Assert.AreEqual("layout-controls-on", NavService.LayoutControls);
            NavService.RouteUserAndUpdateNav(4);
            Assert.AreEqual("", NavService.LayoutControls);

            NavService.ShowLayoutControls(0);
            Assert.AreEqual("layout-controls-on", NavService.LayoutControls);
            NavService.RouteUserAndUpdateNav(0);
            Assert.AreEqual("", NavService.LayoutControls);

            NavService.ShowLayoutControls(2);
            Assert.AreEqual("layout-controls-on", NavService.LayoutControls);
            NavService.RouteUserAndUpdateNav(2);
            Assert.AreEqual("", NavService.LayoutControls);
        }
        // end original ShowLayoutControls() tests
        // start original PlayAnimation() tests
        [TestMethod]
        public void TestPlayAnimationIsNOTContinuousAndCurrentlyOffTurnsAnimationOnForOneRep()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("", NavService.LayoutControls);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.PlayAnimation(1, false);
            Assert.AreEqual("main1", NavService.AnimateMain);
            Assert.AreEqual("", NavService.DiscontinueButton);


            NavService.PlayAnimation(5, false);
            Assert.AreEqual("main5", NavService.AnimateMain);
            Assert.AreEqual("", NavService.DiscontinueButton);

        }
        [TestMethod]
        public void TestPlayAnimationIsNOTContinuousAndCurrentlyOnForOneRepTurnsAnimationOff()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("", NavService.LayoutControls);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.PlayAnimation(1, false);
            Assert.AreEqual("main1", NavService.AnimateMain);
            Assert.AreEqual("", NavService.DiscontinueButton);

            NavService.PlayAnimation(1, false);
            Assert.AreEqual("", NavService.AnimateMain);
            Assert.AreEqual("", NavService.DiscontinueButton);


            NavService.PlayAnimation(2, false);
            Assert.AreEqual("main2", NavService.AnimateMain);
            Assert.AreEqual("", NavService.DiscontinueButton);

            NavService.PlayAnimation(2, false);
            Assert.AreEqual("", NavService.AnimateMain);
            Assert.AreEqual("", NavService.DiscontinueButton);
        }
        [TestMethod]
        public void TestPlayAnimationIsContinuousAndCurrentlyOffTurnsAnimationOnForIndefiniteNumberOfReps()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("", NavService.LayoutControls);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.PlayAnimation(1, true);
            Assert.AreEqual("main1-infinite", NavService.AnimateMain);
            Assert.AreEqual("discontinue-button-on", NavService.DiscontinueButton);


            NavService.PlayAnimation(3, true);
            Assert.AreEqual("main3-infinite", NavService.AnimateMain);
            Assert.AreEqual("discontinue-button-on", NavService.DiscontinueButton);
        }
        [TestMethod]
        public void TestPlayAnimationIsContinuousAndCurrentlyOnTurnsAnimationOff()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("", NavService.LayoutControls);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.PlayAnimation(1, true);
            Assert.AreEqual("main1-infinite", NavService.AnimateMain);
            Assert.AreEqual("discontinue-button-on", NavService.DiscontinueButton);

            NavService.PlayAnimation(1, true);
            Assert.AreEqual("", NavService.AnimateMain);
            Assert.AreEqual("", NavService.DiscontinueButton);


            NavService.PlayAnimation(4, true);
            Assert.AreEqual("main4-infinite", NavService.AnimateMain);
            Assert.AreEqual("discontinue-button-on", NavService.DiscontinueButton);

            NavService.PlayAnimation(4, true);
            Assert.AreEqual("", NavService.AnimateMain);
            Assert.AreEqual("", NavService.DiscontinueButton);
        }
        [TestMethod]
        public void TestPlayAnimationFirstClickNotContinuousAndAnimatedSecondClickIsContinuousAndAnimated()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("", NavService.LayoutControls);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.PlayAnimation(1, false);
            Assert.AreEqual("main1", NavService.AnimateMain);
            Assert.AreEqual("", NavService.DiscontinueButton);

            NavService.PlayAnimation(1, true);
            Assert.AreEqual("main1-infinite", NavService.AnimateMain);
            Assert.AreEqual("discontinue-button-on", NavService.DiscontinueButton);


            NavService.PlayAnimation(5, false);
            Assert.AreEqual("main5", NavService.AnimateMain);
            Assert.AreEqual("", NavService.DiscontinueButton);

            NavService.PlayAnimation(5, true);
            Assert.AreEqual("main5-infinite", NavService.AnimateMain);
            Assert.AreEqual("discontinue-button-on", NavService.DiscontinueButton);
        }
        // end original PlayAnimation() tests
        // start original StopMainAnimation() tests
        [TestMethod]
        public void TestStopMainAnimationOnClickAnimationStops()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("", NavService.LayoutControls);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);


            NavService.PlayAnimation(1, true);
            Assert.AreEqual("main1-infinite", NavService.AnimateMain);
            Assert.AreEqual("discontinue-button-on", NavService.DiscontinueButton);

            NavService.StopMainAnimation();
            Assert.AreEqual("", NavService.AnimateMain);
            Assert.AreEqual("", NavService.DiscontinueButton);


            NavService.PlayAnimation(2, true);
            Assert.AreEqual("main2-infinite", NavService.AnimateMain);
            Assert.AreEqual("discontinue-button-on", NavService.DiscontinueButton);

            NavService.StopMainAnimation();
            Assert.AreEqual("", NavService.AnimateMain);
            Assert.AreEqual("", NavService.DiscontinueButton);
        }
        // end StopMainAnimation() tests
        // start NavigateToCollapsibleSectionOfOtherPage() tests
        [TestMethod]
        public void TestNavigateToCollapsibleSectionOfOtherPagePropertyNavigateToSectionGetsUpdated()
        {
            NavService NavService = new NavService();
            Assert.AreEqual("", NavService.LayoutControls);
            Assert.AreEqual("highlight-button", NavService.AssociatedNav[2].NavButtonStatus);
            Assert.AreEqual("", NavService.AssociatedNav[2].NavPanelStatus);
            Assert.AreEqual(true, NavService.AssociatedNav[2].IsThisLocation);

            // Uses method that is tested above (RouteUserAndUpdateNav()), so skip testing for that. No test duplicates.

            NavService.NavigateToCollapsibleSectionOfOtherPage(2, 6);
            Assert.AreEqual(false, NavService.SectionedPages[2].Sections[6].IsCollapsed);
            Assert.AreEqual(true, NavService.SectionedPages[2].Sections[1].IsCollapsed);
            Assert.AreEqual(true, NavService.SectionedPages[2].Sections[7].IsCollapsed);
            Assert.AreEqual(true, NavService.SectionedPages[2].Sections[5].IsCollapsed);
            Assert.AreEqual(true, NavService.SectionedPages[2].Sections[0].IsCollapsed);
        }
        // end NavigateToCollapsibleSectionOfOtherPage() tests
    }*/
}
