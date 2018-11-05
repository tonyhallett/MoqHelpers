using Moq;
using Moq.Language.Flow;
using MoqHelpers;
using MoqHelpers.Additional;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.NonSequenced;
using MoqHelpers.InSequence.Invocables.Sequenced;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqHelpersTests
{
    [TestFixture]
    public class InSequenceExtensions_Tests
    {
        private MockRepository mockRepository;
        private Mock<IInvocableFactory> mockInvocableFactory;
        private Mock<ISequenceFactory> mockSequenceFactory;
        private Mock<IToMock> mock;

        [SetUp]
        public void SetUp()
        {
            mockRepository = new MockRepository(MockBehavior.Loose);

            mockInvocableFactory = mockRepository.Create<IInvocableFactory>();
            mockSequenceFactory = mockRepository.Create<ISequenceFactory>();
            Invocable.Factory = mockInvocableFactory.Object;
            Sequence.Factory = mockSequenceFactory.Object;

            mock = mockRepository.Create<IToMock>();
        }
        
        #region argument exception message assertions
        private const string passOrThrowsParameterName = "passOrThrows";
        private const string exceptionsOrReturnsParameterName = "exceptionsOrReturns";
        private const string returnsParameterName = "returns";

        private const string sequenceParameterName = "sequence";
        private const string sequenceInvocationIndicesParameterName = "sequenceInvocationIndices";
        
        private void Assert_ConsecutiveInvocations_Less_Than_1_Throws(TestDelegate action)
        {
            Assert.That(action, Throws.Exception.TypeOf<ArgumentException>().With.Message.EqualTo(InSequenceExtensions.ConsecutiveInvocationsLessThanOneMessage));
        }
        private void Assert_Loops_Less_Than_1_Throws(TestDelegate action)
        {
            Assert.That(action, Throws.Exception.TypeOf<ArgumentException>().With.Message.EqualTo(InSequenceExtensions.LoopsLessThanOneMessage));
        }
        private void Assert_Null_SequenceInvocationIndices_Throws(TestDelegate action)
        {
            Assert.That(action, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo(sequenceInvocationIndicesParameterName));
        }
        private void Assert_Empty_SequenceInvocationIndices_Throws(TestDelegate action)
        {
            Assert.That(action, Throws.Exception.TypeOf<ArgumentException>().With.Message.EqualTo(InSequenceExtensions.SequenceInvocationIndicesEmptyMessage));
        }

        private void Assert_Null_Responses_Throws(TestDelegate action,string paramName)
        {
            Assert.That(action, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo(paramName));
        }
        private void Assert_Null_PassOrThrows_Throws(TestDelegate action)
        {
            Assert_Null_Responses_Throws(action, passOrThrowsParameterName);
        }
        private void Assert_Null_Returns_Throws(TestDelegate action)
        {
            Assert_Null_Responses_Throws(action, returnsParameterName);
        }
        private void Assert_Null_ExceptionsOrReturns_Throws(TestDelegate action)
        {
            Assert_Null_Responses_Throws(action, exceptionsOrReturnsParameterName);
        }

        private void Assert_Empty_Responses_Throws(TestDelegate action)
        {
            Assert.That(action, Throws.Exception.TypeOf<ArgumentException>().With.Message.EqualTo(InSequenceExtensions.InvocationResponsesEmptyMessage));
        }
        private void Assert_Incorrect_Number_Of_ConfiguredResponses_Throws(TestDelegate action)
        {
            Assert.That(action, Throws.Exception.TypeOf<ArgumentException>().With.Message.EqualTo(InSequenceExtensions.IncorrectNumberOfConfiguredResponses));
        }
        private void Assert_Null_Sequence_Throws(TestDelegate action)
        {
            Assert.That(action, Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo(sequenceParameterName));
        }
        #endregion

        
        #region setupGet
        [Test]
        public void SetupGetInSequenceSingle()
        {
            var consecutiveInvocations = 3;
            var loops = 2;

            var mockedInvocableGetter = mockRepository.Create<IInvocableGet<IToMock, int>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableGetter)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();

            
            mockInvocableFactory.Setup(m => m.CreateGet<IToMock, int>(It.IsNotNull<ISetupGetter<IToMock, int>>(), mock, sequence, consecutiveInvocations))
                .Returns(mockedInvocableGetter).Verifiable();

            
            var wrapped = mock.SetupGetInSequenceSingle(m => m.Prop,consecutiveInvocations,loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableGetter));
        }
        [Test]
        public void SetupGetInSequenceSingle_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupGetInSequenceSingle(m => m.Prop, 1, 0));
        }
        [Test]
        public void SetupGetInSequenceSingle_Throws_With_ConsectiveInvocations_Less_Than_1()
        {
            Assert_ConsecutiveInvocations_Less_Than_1_Throws(() => mock.SetupGetInSequenceSingle(m => m.Prop, 0, 1));
        }

        [Test]
        public void SetupGetInSequenceSingle_SequenceInvocationIndices()
        {
            var SequenceInvocationIndices = new SequenceInvocationIndices { 1, 2, 3 };
            var loops = 2;

            var mockedInvocableGetter = mockRepository.Create<IInvocableGet<IToMock, int>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableGetter)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();

            mockInvocableFactory.Setup(m => m.CreateGet<IToMock, int>(It.IsNotNull<ISetupGetter<IToMock, int>>(), mock, sequence, SequenceInvocationIndices))
                .Returns(mockedInvocableGetter).Verifiable();

            var wrapped = mock.SetupGetInSequenceSingle(m => m.Prop, SequenceInvocationIndices, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableGetter));

        }
        [Test]
        public void SetupGetInSequenceSingle_SequenceInvocationIndices_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupGetInSequenceSingle(m => m.Prop, new SequenceInvocationIndices { 0}, -1));
        }
        [Test] 
        public void SetupGetInSequenceSingle_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupGetInSequenceSingle(m => m.Prop, null, 1));
        }
        [Test]
        public void SetupGetInSequenceSingle_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupGetInSequenceSingle(m => m.Prop, new SequenceInvocationIndices { }, 1));
        }
        
        [Test]
        public void SetupGetInSequenceSharedCreate()
        {
            var consecutiveInvocations = 1;
            var loops = 1;

            var mockedInvocableGetter = mockRepository.Create<IInvocableGet<IToMock, int>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableGetter)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared(loops)).Returns(sequence).Verifiable();
            
            mockInvocableFactory.Setup(m => m.CreateGet<IToMock, int>(It.IsNotNull<ISetupGetter<IToMock, int>>(), mock, sequence, consecutiveInvocations))
                .Returns(mockedInvocableGetter).Verifiable();

            var wrapped = mock.SetupGetInSequenceCreateShared(m => m.Prop, out var createdSequence, consecutiveInvocations, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableGetter));
            Assert.That(createdSequence, Is.EqualTo(sequence));
        }
        [Test]
        public void SetupGetInSequenceSharedCreate_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(()=> mock.SetupGetInSequenceCreateShared(m => m.Prop, out var createdSequence, 1, 0));
        }
        [Test]
        public void SetupGetInSequenceSharedCreate_Throws_With_ConsectiveInvocations_Less_Than_1()
        {
            Assert_ConsecutiveInvocations_Less_Than_1_Throws(() => mock.SetupGetInSequenceCreateShared(m => m.Prop, out var createdSequence,0, 1));
        }
        
        [Test]
        public void SetupGetInSequenceSharedCreate_SequenceInvocationIndices()
        {
            var SequenceInvocationIndices = new SequenceInvocationIndices { 0};
            var loops = 1;

            var mockedInvocableGetter = mockRepository.Create<IInvocableGet<IToMock, int>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableGetter)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared(loops)).Returns(sequence).Verifiable();

            mockInvocableFactory.Setup(m => m.CreateGet<IToMock, int>(It.IsNotNull<ISetupGetter<IToMock, int>>(), mock, sequence, SequenceInvocationIndices))
                .Returns(mockedInvocableGetter).Verifiable();

            var wrapped = mock.SetupGetInSequenceCreateShared(m => m.Prop, out var createdSequence, SequenceInvocationIndices, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableGetter));
            Assert.That(createdSequence, Is.EqualTo(sequence));
        }
        [Test]
        public void SetupGetInSequenceSharedCreate_SequenceInvocationIndices_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(()=> mock.SetupGetInSequenceCreateShared(m => m.Prop, out var createdSequence, new SequenceInvocationIndices { 1}, 0));
        }
        [Test]
        public void SetupGetInSequenceSharedCreate_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            Assert_Null_SequenceInvocationIndices_Throws (() => mock.SetupGetInSequenceCreateShared(m => m.Prop, out var createdSequence, null, 1));
        }
        [Test]
        public void SetupGetInSequenceSharedCreate_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupGetInSequenceCreateShared(m => m.Prop, out var createdSequence, new SequenceInvocationIndices { }, 1));
        }

        [Test]
        public void SetupGetInSequenceShared()
        {
            var consecutiveInvocations = 2;

            var mockedInvocableGetter = mockRepository.Create<IInvocableGet<IToMock, int>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableGetter)).Verifiable();
            var sequence = mockSequence.Object;
            
            mockInvocableFactory.Setup(m => m.CreateGet<IToMock, int>(It.IsNotNull<ISetupGetter<IToMock, int>>(), mock, sequence, consecutiveInvocations))
                .Returns(mockedInvocableGetter).Verifiable();

            var wrapped = mock.SetupGetInSequenceShared(m => m.Prop, sequence, consecutiveInvocations);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableGetter));
        }
        [Test]
        public void SetupGetInSequenceShared_Throws_With_ConsectiveInvocations_Less_Than_1()
        {
            var sequence = new Sequence(0);
            Assert_ConsecutiveInvocations_Less_Than_1_Throws(() => mock.SetupGetInSequenceShared(m => m.Prop, sequence, 0));
        }
        [Test]
        public void SetupGetInSequenceShared_Throws_With_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupGetInSequenceShared(m => m.Prop, null, 1));
        }
        [Test]
        public void SetupGetInSequenceShared_SequenceInvocationIndices()
        {
            var SequenceInvocationIndices = new SequenceInvocationIndices { 0 };

            var mockedInvocableGetter = mockRepository.Create<IInvocableGet<IToMock, int>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableGetter)).Verifiable();
            var sequence = mockSequence.Object;

            mockInvocableFactory.Setup(m => m.CreateGet<IToMock, int>(It.IsNotNull<ISetupGetter<IToMock, int>>(), mock, sequence, SequenceInvocationIndices))
                .Returns(mockedInvocableGetter).Verifiable();

            var wrapped = mock.SetupGetInSequenceShared(m => m.Prop, sequence, SequenceInvocationIndices);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableGetter));

        }
        [Test]
        public void SetupGetInSequenceShared_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupGetInSequenceShared(m => m.Prop, sequence, null));
        }
        [Test]
        public void SetupGetInSequenceShared_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupGetInSequenceShared(m => m.Prop, sequence, new SequenceInvocationIndices { }));
        }
        [Test]
        public void SetupGetInSequenceShared__SequenceInvocationIndices_Throws_With_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupGetInSequenceShared(m => m.Prop, null, new SequenceInvocationIndices { 1 }));
        }

        #endregion

        #region setupSet
        [Test]
        public void SetupSetInSequenceSingle()
        {
            var consecutiveInvocations = 2;
            var loops = 3;

            var mockedInvocableSetter = mockRepository.Create<IInvocableNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSetter)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();

            mockInvocableFactory.Setup(m => m.CreateNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, consecutiveInvocations))
                .Returns(mockedInvocableSetter).Verifiable();

            var wrapped = mock.SetupSetInSequenceSingle(m => m.Prop = 1, consecutiveInvocations, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSetter));
        }
        [Test]
        public void SetupSetInSequenceSingle_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupSetInSequenceSingle(m => m.Prop = 1, 1, 0));
        }
        [Test]
        public void SetupSetInSequenceSingle_Throws_With_ConsectiveInvocations_Less_Than_1()
        {
            Assert_ConsecutiveInvocations_Less_Than_1_Throws(() => mock.SetupSetInSequenceSingle(m => m.Prop = 1, 0, 1));
        }

        [Test]
        public void SetupSetInSequenceSingle_SequenceInvocationIndices()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1 };
            var loops = 3;

            var mockedInvocableSetter = mockRepository.Create<IInvocableNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSetter)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();

            
            mockInvocableFactory.Setup(m => m.CreateNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, sequenceInvocationIndices))
                .Returns(mockedInvocableSetter).Verifiable();

            var wrapped = mock.SetupSetInSequenceSingle(m => m.Prop = 1, sequenceInvocationIndices, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSetter));

        }
        [Test]
        public void SetupSetInSequenceSingle_SequenceInvocationIndices_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupSetInSequenceSingle(m => m.Prop = 1, new SequenceInvocationIndices {1 }, 0));
        }
        [Test]
        public void SetupSetInSequenceSingle_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupSetInSequenceSingle(m => m.Prop = 1,null, 1));
        }
        [Test]
        public void SetupSetInSequenceSingle_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupSetInSequenceSingle(m => m.Prop = 1, new SequenceInvocationIndices { }, 1));
        }

        [Test]
        public void SetupSetInSequenceSharedCreate()
        {
            var consecutiveInvocations = 2;
            var loops = 3;

            var mockedInvocableSetter = mockRepository.Create<IInvocableNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSetter)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared(loops)).Returns(sequence).Verifiable();

            mockInvocableFactory.Setup(m => m.CreateNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, consecutiveInvocations))
                .Returns(mockedInvocableSetter).Verifiable();

            var wrapped = mock.SetupSetInSequenceCreateShared(m => m.Prop = 1, out var createdSequence, consecutiveInvocations, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSetter));
            Assert.That(createdSequence, Is.EqualTo(sequence));
        }
        [Test]
        public void SetupSetInSequenceSharedCreate_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupSetInSequenceCreateShared(m => m.Prop = 1, out var createdSequence, 1, 0));
        }
        [Test]
        public void SetupSetInSequenceSharedCreate_Throws_With_ConsectiveInvocations_Less_Than_1()
        {
            Assert_ConsecutiveInvocations_Less_Than_1_Throws(()=> mock.SetupSetInSequenceCreateShared(m => m.Prop = 1, out var createdSequence, 0, 1));
        }

        [Test]
        public void SetupSetInSequenceSharedCreate_SequenceInvocationIndices()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1 };
            var loops = 3;

            var mockedInvocableSetter = mockRepository.Create<IInvocableNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSetter)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared(loops)).Returns(sequence).Verifiable();

            
            mockInvocableFactory.Setup(m => m.CreateNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, sequenceInvocationIndices))
                .Returns(mockedInvocableSetter).Verifiable();

            var wrapped = mock.SetupSetInSequenceCreateShared(m => m.Prop = 1, out var createdSequence, sequenceInvocationIndices, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSetter));
            Assert.That(createdSequence, Is.EqualTo(sequence));
        }
        [Test]
        public void SetupSetInSequenceSharedCreate_SequenceInvocationIndices_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupSetInSequenceCreateShared(m => m.Prop = 1, out var createdSequence, new SequenceInvocationIndices { 1 }, 0));
        }
        [Test]
        public void SetupSetInSequenceSharedCreate_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupSetInSequenceCreateShared(m => m.Prop = 1, out var createdSequence,null, 1));
        }
        [Test]
        public void SetupSetInSequenceSharedCreate_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupSetInSequenceCreateShared(m => m.Prop = 1, out var createdSequence, new SequenceInvocationIndices { }, 1));
        }

        [Test]
        public void SetupSetInSequenceShared()
        {
            var consecutiveInvocations = 2;

            var mockedInvocableSetter = mockRepository.Create<IInvocableNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSetter)).Verifiable();
            var sequence = mockSequence.Object;

            
            mockInvocableFactory.Setup(m => m.CreateNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, consecutiveInvocations))
                .Returns(mockedInvocableSetter).Verifiable();

            var wrapped = mock.SetupSetInSequenceShared(m => m.Prop = 1, sequence, consecutiveInvocations);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSetter));
        }
        [Test]
        public void SetupSetInSequenceShared_Throws_With_ConsectiveInvocations_Less_Than_1()
        {
            var sequence = new Sequence(0);
            Assert_ConsecutiveInvocations_Less_Than_1_Throws(() => mock.SetupSetInSequenceShared(m => m.Prop = 1, sequence, 0));
        }
        [Test]
        public void SetupSetInSequenceShared_Throws_With_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupSetInSequenceShared(m => m.Prop = 1, null, 0));
        }

        [Test]
        public void SetupSetInSequenceShared_SequenceInvocationIndices()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1 };

            var mockedInvocableSetter = mockRepository.Create<IInvocableNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSetter)).Verifiable();
            var sequence = mockSequence.Object;

            
            mockInvocableFactory.Setup(m => m.CreateNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, sequenceInvocationIndices))
                .Returns(mockedInvocableSetter).Verifiable();

            var wrapped = mock.SetupSetInSequenceShared(m => m.Prop = 1, sequence, sequenceInvocationIndices);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSetter));
        }
        [Test]
        public void SetupSetInSequenceShared_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupSetInSequenceShared(m => m.Prop = 1, sequence, null));
        }
        [Test]
        public void SetupSetInSequenceShared_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupSetInSequenceShared(m => m.Prop = 1, sequence, new SequenceInvocationIndices { }));
        }
        [Test]
        public void SetupSetInSequenceShared__SequenceInvocationIndices_Throws_With_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupSetInSequenceShared(m => m.Prop = 1, null, new SequenceInvocationIndices { 1 }));
        }

        #endregion

        #region setupNoResult
        [Test]
        public void SetupInSequenceSingle()
        {
            var loops = 5;
            var consecutiveInvocations = 2;

            var mockedNoReturn = new Mock<IInvocableNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedNoReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();

            
            mockInvocableFactory.Setup(m => m.CreateNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, consecutiveInvocations)).Returns(mockedNoReturn).Verifiable();

            var wrapped = mock.SetupInSequenceSingle(m => m.Method1(), consecutiveInvocations, loops);
            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedNoReturn));
        }
        [Test]
        public void SetupInSequenceSingle_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceSingle(m => m.Method1(), 1, 0));
        }
        [Test]
        public void SetupInSequenceSingle_Throws_With_ConsectiveInvocations_Less_Than_1()
        {
            Assert_ConsecutiveInvocations_Less_Than_1_Throws(() => mock.SetupInSequenceSingle(m => m.Method1(), 0, 1));
        }

        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices()
        {
            var loops = 5;
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1, 2, 5 };

            var mockedNoReturn = new Mock<IInvocableNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedNoReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();

            
            mockInvocableFactory.Setup(m => m.CreateNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, sequenceInvocationIndices)).Returns(mockedNoReturn).Verifiable();

            var wrapped = mock.SetupInSequenceSingle(m => m.Method1(), sequenceInvocationIndices, loops);
            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedNoReturn));

        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(()=>mock.SetupInSequenceSingle(m => m.Method1(), new SequenceInvocationIndices { 1}, 0));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceSingle(m => m.Method1(), (SequenceInvocationIndices)null, 1));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceSingle(m => m.Method1(), new SequenceInvocationIndices { }, 1));
        }

        [Test]
        public void SetupInSequenceSharedCreate()
        {
            var loops = 5;
            var consecutiveInvocations = 2;

            var mockedNoReturn = new Mock<IInvocableNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedNoReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared(loops)).Returns(sequence).Verifiable();

            
            mockInvocableFactory.Setup(m => m.CreateNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, consecutiveInvocations)).Returns(mockedNoReturn).Verifiable();

            var wrapped = mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence, consecutiveInvocations, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedNoReturn));
            Assert.That(createdSequence, Is.EqualTo(sequence));
        }
        [Test]
        public void SetupInSequenceSharedCreate_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(()=>mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence, 1, 0));
        }
        [Test]
        public void SetupInSequenceSharedCreate_Throws_With_ConsectiveInvocations_Less_Than_1()
        {
            Assert_ConsecutiveInvocations_Less_Than_1_Throws(() => mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence, 0, 1));
        }

        [Test]
        public void SetupInSequenceSharedCreate_SequenceInvocationIndices()
        {
            var loops = 5;
            var SequenceInvocationIndices = new SequenceInvocationIndices { 1, 29 };

            var mockedNoReturn = new Mock<IInvocableNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedNoReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared(loops)).Returns(sequence).Verifiable();

            
            mockInvocableFactory.Setup(m => m.CreateNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, SequenceInvocationIndices)).Returns(mockedNoReturn).Verifiable();

            var wrapped = mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence, SequenceInvocationIndices, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedNoReturn));
            Assert.That(createdSequence, Is.EqualTo(sequence));

        }
        [Test]
        public void SetupInSequenceSharedCreate_SequenceInvocationIndices_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(()=>mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence, new SequenceInvocationIndices { 1}, 0));
        }
        [Test]
        public void SetupInSequenceSharedCreate_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence, (SequenceInvocationIndices)null, 1));
        }
        [Test]
        public void SetupInSequenceSharedCreate_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence, new SequenceInvocationIndices {  }, 1));
        }

        [Test]
        public void SetupInSequenceShared()
        {
            var consecutiveInvocations = 2;

            var mockedNoReturn = new Mock<IInvocableNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedNoReturn)).Verifiable();
            var sequence = mockSequence.Object;

           
            mockInvocableFactory.Setup(m => m.CreateNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, consecutiveInvocations)).Returns(mockedNoReturn).Verifiable();

            var wrapped = mock.SetupInSequenceShared(m => m.Method1(), sequence, consecutiveInvocations);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedNoReturn));
        }
        [Test]
        public void SetupInSequenceShared_Throws_With_ConsectiveInvocations_Less_Than_1()
        {
            var sequence = new Sequence(0);
            Assert_ConsecutiveInvocations_Less_Than_1_Throws(()=>mock.SetupInSequenceShared(m => m.Method1(), sequence, 0));
        }
        [Test]
        public void SetupInSequenceShared_Throws_With_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupInSequenceShared(m => m.Method1(), null, 1));
        }

        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1 };

            var mockedNoReturn = new Mock<IInvocableNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedNoReturn)).Verifiable();
            var sequence = mockSequence.Object;

            mockInvocableFactory.Setup(m => m.CreateNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, sequenceInvocationIndices)).Returns(mockedNoReturn).Verifiable();

            var wrapped = mock.SetupInSequenceShared(m => m.Method1(), sequence, sequenceInvocationIndices);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedNoReturn));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupGetInSequenceShared(m => m.Prop, sequence, null));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupGetInSequenceShared(m => m.Prop, sequence, new SequenceInvocationIndices { }));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_Throws_With_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupGetInSequenceShared(m => m.Prop, null, new SequenceInvocationIndices { 1 }));
        }


        #endregion

        #region setupResult
        [Test]
        public void SetupInSequenceSingle_Result()
        {
            var loops = 5;
            var consecutiveInvocations = 2;

            var mockedReturn = new Mock<IInvocableReturn<IToMock, string>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();

            
            mockInvocableFactory.Setup(m => m.CreateReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, consecutiveInvocations)).Returns(mockedReturn).Verifiable();

            var wrapped = mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), consecutiveInvocations, loops);
            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedReturn));
        }
        [Test]
        public void SetupInSequenceSingle_Result_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), 1, 0));
        }
        [Test]
        public void SetupInSequenceSingle_Result_Throws_With_ConsectiveInvocations_Less_Than_1()
        {
            Assert_ConsecutiveInvocations_Less_Than_1_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), 0, 1));
        }

        [Test]
        public void SetupInSequenceSingle_Result_SequenceInvocationIndices()
        {
            var loops = 5;
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1 };

            var mockedReturn = new Mock<IInvocableReturn<IToMock, string>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();

            
            mockInvocableFactory.Setup(m => m.CreateReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, sequenceInvocationIndices)).Returns(mockedReturn).Verifiable();

            var wrapped = mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), sequenceInvocationIndices, loops);
            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedReturn));

        }
        [Test]
        public void SetupInSequenceSingle_Result_SequenceInvocationIndices_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new SequenceInvocationIndices { 1}, 0));
        }
        [Test]
        public void SetupInSequenceSingle_Result_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), (SequenceInvocationIndices)null, 1));
        }
        [Test]
        public void SetupInSequenceSingle_Result_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new SequenceInvocationIndices { }, 1));
        }

        [Test]
        public void SetupInSequenceSharedCreate_Result()
        {
            var loops = 5;
            var consecutiveInvocations = 2;

            var mockedReturn = new Mock<IInvocableReturn<IToMock, string>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedReturn));
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared(loops)).Returns(sequence).Verifiable();

            
            mockInvocableFactory.Setup(m => m.CreateReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, consecutiveInvocations)).Returns(mockedReturn).Verifiable();

            var wrapped = mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""), out var createdSequence, consecutiveInvocations, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedReturn));
            Assert.That(createdSequence, Is.EqualTo(sequence));
        }
        [Test]
        public void SetupInSequenceSharedCreate_Result_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""), out var createdSequence, 1, 0));
        }
        [Test]
        public void SetupInSequenceSharedCreate_Result_Throws_With_ConsectiveInvocations_Less_Than_1()
        {
            Assert_ConsecutiveInvocations_Less_Than_1_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""), out var createdSequence, 0, 1));
        }

        [Test]
        public void SetupInSequenceSharedCreate_Result_SequenceInvocationIndices()
        {
            var loops = 5;
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1 };

            var mockedReturn = new Mock<IInvocableReturn<IToMock, string>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared(loops)).Returns(sequence).Verifiable();

            
            mockInvocableFactory.Setup(m => m.CreateReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, sequenceInvocationIndices)).Returns(mockedReturn).Verifiable();

            var wrapped = mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""), out var createdSequence, sequenceInvocationIndices, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedReturn));
            Assert.That(createdSequence, Is.EqualTo(sequence));
        }
        [Test]
        public void SetupInSequenceSharedCreate_Result_SequenceInvocationIndices_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""), out var createdSequence, new SequenceInvocationIndices { 1 }, 0));
        }
        [Test]
        public void SetupInSequenceSharedCreate_Result_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""), out var createdSequence, (SequenceInvocationIndices)null, 1));
        }
        [Test]
        public void SetupInSequenceSharedCreate_Result_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""), out var createdSequence, new SequenceInvocationIndices { }, 1));
        }

        [Test]
        public void SetupInSequenceShared_Result()
        {
            var consecutiveInvocations = 2;

            var mockedReturn = new Mock<IInvocableReturn<IToMock, string>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedReturn)).Verifiable();
            var sequence = mockSequence.Object;

            
            mockInvocableFactory.Setup(m => m.CreateReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, consecutiveInvocations)).Returns(mockedReturn).Verifiable();

            var wrapped = mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, consecutiveInvocations);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedReturn));
        }
        [Test]
        public void SetupInSequenceShared_Result_Throws_With_ConsectiveInvocations_Less_Than_1()
        {
            var sequence = new Sequence(0);
            Assert_ConsecutiveInvocations_Less_Than_1_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, 0));
        }
        [Test]
        public void SetupInSequenceShared_Result_Throws_With_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), null, 1));
        }

        [Test]
        public void SetupInSequenceShared_Result_SequenceInvocationIndices()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1 };

            var mockedReturn = new Mock<IInvocableReturn<IToMock, string>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedReturn)).Verifiable();
            var sequence = mockSequence.Object;

            
            mockInvocableFactory.Setup(m => m.CreateReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, sequenceInvocationIndices)).Returns(mockedReturn).Verifiable();

            var wrapped = mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, sequenceInvocationIndices);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedReturn));
        }
        [Test]
        public void SetupInSequenceShared_Result_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, (SequenceInvocationIndices)null));
        }
        [Test]
        public void SetupInSequenceShared_Result_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, new SequenceInvocationIndices { }));
        }
        [Test]
        public void SetupInSequenceShared_Result_SequenceInvocationIndices_Throws_With_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), null, new SequenceInvocationIndices { 1 }));
        }

        #endregion

        #region sequence in sequence
        #region no return
        [Test]
        public void SetupInSequenceSingle_PassOrThrows()
        {
            var loops = 2;

            var mockedInvocableSequenceNoReturn = new Mock<IInvocableSequenceNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceNoReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();

            var passOrThrows = new PassOrThrows { new Exception(), null, new ArgumentException() };

            
            mockInvocableFactory.Setup(m => m.CreateSequenceNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, passOrThrows))
                .Returns(mockedInvocableSequenceNoReturn).Verifiable();

            var wrapped = mock.SetupInSequenceSingle(m => m.Method1(), passOrThrows, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceNoReturn));
        }
        [Test]
        public void SetupInSequenceSingle_PassOrThrows_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceSingle(m => m.Method1(), new PassOrThrows { null}, 0));
        }
        [Test]
        public void SetupInSequenceSingle_PassOrThrows_Throws_With_Null_PassOrThrows()
        {
            Assert_Null_PassOrThrows_Throws(() => mock.SetupInSequenceSingle(m => m.Method1(), (PassOrThrows)null, 1));
        }
        [Test]
        public void SetupInSequenceSingle_PassOrThrows_Throws_With_Empty_PassOrThrows()
        {
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceSingle(m => m.Method1(), new PassOrThrows {  }, 1));
        }

        [Test]
        public void SetupInSequenceCreateShared_PassOrThrows()
        {
            var loops = 2;

            var mockedInvocableSequenceNoReturn = new Mock<IInvocableSequenceNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceNoReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared(loops)).Returns(sequence).Verifiable();

            var passOrThrows = new PassOrThrows { new Exception(), null, new ArgumentException() };

            
            mockInvocableFactory.Setup(m => m.CreateSequenceNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, passOrThrows))
                .Returns(mockedInvocableSequenceNoReturn).Verifiable();

            var wrapped = mock.SetupInSequenceCreateShared(m => m.Method1(),out var createdSequence, passOrThrows, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceNoReturn));
            Assert.That(createdSequence, Is.EqualTo(sequence));
        }
        [Test]
        public void SetupInSequenceCreateShared_PassOrThrows_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence, new PassOrThrows { null }, 0));
        }
        [Test]
        public void SetupInSequenceCreateShared_PassOrThrows_Throws_With_Null_PassOrThrows()
        {
            Assert_Null_PassOrThrows_Throws(() => mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence,(PassOrThrows) null,1));
        }
        [Test]
        public void SetupInSequenceCreateShared_PassOrThrows_Throws_With_Empty_PassOrThrows()
        {
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence, new PassOrThrows { }, 1));
        }

        [Test]
        public void SetupInSequenceShared_PassOrThrows()
        {
            var mockedInvocableSequenceNoReturn = new Mock<IInvocableSequenceNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceNoReturn)).Verifiable();
            var sequence = mockSequence.Object;

            var passOrThrows = new PassOrThrows { new Exception(), null, new ArgumentException() };

            
            mockInvocableFactory.Setup(m => m.CreateSequenceNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, passOrThrows))
                .Returns(mockedInvocableSequenceNoReturn).Verifiable();

            var wrapped = mock.SetupInSequenceShared(m => m.Method1(), sequence, passOrThrows);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceNoReturn));
        }
        [Test]
        public void SetupInSequenceShared_PassOrThrows_Throws_With_Null_PassOrThrows()
        {
            var sequence = new Sequence(0);
            Assert_Null_PassOrThrows_Throws(() => mock.SetupInSequenceShared(m => m.Method1(), sequence, (PassOrThrows)null));
        }
        [Test]
        public void SetupInSequenceShared_PassOrThrows_Throws_With_Empty_PassOrThrows()
        {
            var sequence = new Sequence(0);
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceShared(m => m.Method1(), sequence, new PassOrThrows()));
        }
        [Test]
        public void SetupInSequenceShared_PassOrThrows_Throws_With_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupInSequenceShared(m => m.Method1(), null, new PassOrThrows { null}));
        }

        [Test]
        public void SetupInSequenceSingle_PassOrThrows_SequenceInvocationIndices()
        {
            var loops = 2;
            var sequenceInvocationIndices = new SequenceInvocationIndices { 0, 1, 2 };

            var mockedInvocableSequenceNoReturn = new Mock<IInvocableSequenceNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceNoReturn)).Verifiable();
            var sequence = mockSequence.Object;

            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();

            var passOrThrows = new PassOrThrows { new Exception(), null, new ArgumentException() };

            
            mockInvocableFactory.Setup(m => m.CreateSequenceNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, passOrThrows, sequenceInvocationIndices))
                .Returns(mockedInvocableSequenceNoReturn).Verifiable();

            var wrapped = mock.SetupInSequenceSingle(m => m.Method1(), passOrThrows, sequenceInvocationIndices, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceNoReturn));
        }
        [Test]
        public void SetupInSequenceSingle_PassOrThrows__SequenceInvocationIndices_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceSingle(m => m.Method1(), new PassOrThrows { null },new SequenceInvocationIndices { 1 }, 0));
        }
        [Test]
        public void SetupInSequenceSingle_PassOrThrows__SequenceInvocationIndices_Throws_With_Null_PassOrThrows()
        {
            Assert_Null_PassOrThrows_Throws(() => mock.SetupInSequenceSingle(m => m.Method1(), (PassOrThrows)null, new SequenceInvocationIndices { }, 1));
        }
        [Test]
        public void SetupInSequenceSingle_PassOrThrows_SequenceInvocationIndices_Throws_With_Empty_PassOrThrows()
        {
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceSingle(m => m.Method1(), new PassOrThrows { },new SequenceInvocationIndices { },1));
        }
        [Test]
        public void SetupInSequenceSingle_PassOrThrows_SequenceInvocationIndices_Throws_With_Null_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""),new PassOrThrows { null }, (SequenceInvocationIndices)null));
        }
        [Test]
        public void SetupInSequenceSingle_PassOrThrows_SequenceInvocationIndices_Throws_With_Empty_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""),new PassOrThrows { null}, new SequenceInvocationIndices { }));
        }
        [Test]
        public void SetupInSequenceSingle_PassOrThrows_SequenceInvocationIndices_Throws_With_Incorrect_Number_Of_Responses()
        {
            var sequence = new Sequence(0);
            Assert_Incorrect_Number_Of_ConfiguredResponses_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new PassOrThrows { null }, new SequenceInvocationIndices { 1,2}));
        }


        [Test]
        public void SetupInSequenceCreateShared__SequenceInvocationIndices_PassOrThrows()
        {
            var loops = 2;
            var sequenceInvocationIndices = new SequenceInvocationIndices { 0, 1, 2 };

            var mockedInvocableSequenceNoReturn = new Mock<IInvocableSequenceNoReturn<IToMock>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceNoReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared( loops)).Returns(sequence).Verifiable();

            var passOrThrows = new PassOrThrows { new Exception(), null, new ArgumentException() };

            

            mockInvocableFactory.Setup(m => m.CreateSequenceNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, passOrThrows, sequenceInvocationIndices))
                .Returns(mockedInvocableSequenceNoReturn).Verifiable();

            var wrapped = mock.SetupInSequenceCreateShared(m => m.Method1(),out var createdSequence, passOrThrows, sequenceInvocationIndices, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceNoReturn));
            Assert.That(createdSequence, Is.EqualTo(sequence));
        }
        [Test]
        public void SetupInSequenceCreateShared__SequenceInvocationIndices_PassOrThrows_Throws_With_Loops_Less_Than_1()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence, new PassOrThrows { null },new SequenceInvocationIndices { 1 }, 0));
        }
        [Test]
        public void SetupInSequenceCreateShared__SequenceInvocationIndices_PassOrThrows_Throws_With_Null_PassOrThrows()
        {
            Assert_Null_PassOrThrows_Throws(() => mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence, (PassOrThrows)null, new SequenceInvocationIndices { 1 }, 1));
        }
        [Test]
        public void SetupInSequenceCreateShared__SequenceInvocationIndices_PassOrThrows_Throws_With_Empty_PassOrThrows()
        {
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceCreateShared(m => m.Method1(), out var createdSequence, new PassOrThrows { }, new SequenceInvocationIndices { 1 }, 1));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_PassOrThrows_Throws_With_Null_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var createdSequence, new PassOrThrows { null }, (SequenceInvocationIndices)null,1));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_PassOrThrows_Throws_With_Empty_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var createdSequence, new PassOrThrows { null }, new SequenceInvocationIndices { },1));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_PassOrThrows_Throws_With_Incorrect_Number_Of_Responses()
        {
            var sequence = new Sequence(0);
            Assert_Incorrect_Number_Of_ConfiguredResponses_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var createdSequence, new PassOrThrows { null }, new SequenceInvocationIndices { 1, 2 },1));
        }

        [Test]
        public void SetupInSequenceShared__SequenceInvocationIndices_PassOrThrows()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices { 0, 1, 2 };

            var mockedInvocableSequenceNoReturn = new Mock<IInvocableSequenceNoReturn<IToMock>>().Object;
            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceNoReturn)).Verifiable();
            var sequence = mockSequence.Object;

            var passOrThrows = new PassOrThrows { new Exception(), null, new ArgumentException() };

            
            mockInvocableFactory.Setup(m => m.CreateSequenceNoReturn(It.IsNotNull<ISetup<IToMock>>(), mock, sequence, passOrThrows, sequenceInvocationIndices))
                .Returns(mockedInvocableSequenceNoReturn).Verifiable();

            var wrapped = mock.SetupInSequenceShared(m => m.Method1(), sequence, passOrThrows, sequenceInvocationIndices);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceNoReturn));
        }
        [Test]
        public void SetupInSequenceShared__SequenceInvocationIndices_PassOrThrows_Throws_With_Null_PassOrThrows()
        {
            var sequence = new Sequence(0);
            Assert_Null_PassOrThrows_Throws(() => mock.SetupInSequenceShared(m => m.Method1(), sequence, (PassOrThrows)null,new SequenceInvocationIndices { 1 }));
        }
        [Test]
        public void SetupInSequenceShared__SequenceInvocationIndices_PassOrThrows_Throws_With_Empty_PassOrThrows()
        {
            var sequence = new Sequence(0);
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceShared(m => m.Method1(), sequence, new PassOrThrows(),new SequenceInvocationIndices { 1 }));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_PassOrThrows_Throws_With_Null_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, new PassOrThrows { null }, (SequenceInvocationIndices)null));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_PassOrThrows_Throws_With_Empty_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""),sequence, new PassOrThrows { null }, new SequenceInvocationIndices { }));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_PassOrThrows_Throws_With_Incorrect_Number_Of_Responses()
        {
            var sequence = new Sequence(0);
            Assert_Incorrect_Number_Of_ConfiguredResponses_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, new PassOrThrows { null }, new SequenceInvocationIndices { 1, 2 }));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_PassOrThrows_Throws_With_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), null, new PassOrThrows { null }, new SequenceInvocationIndices { 1 }));
        }


        #endregion
        #region return
        [Test]
        public void SetupInSequenceSingle_ExceptionsOrReturns()
        {
            var loops = 1;

            var mockedInvocableSequenceReturn = new Mock<IInvocableSequenceReturn<IToMock,string,ExceptionOrReturn<string>>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();
            ExceptionsOrReturns<string> exceptionsOrReturns = new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Exception<string>(new Exception()) };
            
            mockInvocableFactory.Setup(m => m.CreateSequenceReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, exceptionsOrReturns)).Returns(mockedInvocableSequenceReturn).Verifiable();

            var wrapped = mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), exceptionsOrReturns, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceReturn));

        }
        [Test]
        public void SetupInSequenceSingle_ExceptionsOrReturns_Throws_Loops_Less_Than_One()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, 0));
        }
        [Test]
        public void SetupInSequenceSingle_ExceptionsOrReturns_Throws_Null_ExceptionsOrReturns()
        {
            Assert_Null_ExceptionsOrReturns_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), (ExceptionsOrReturns<string>)null, 1));
        }
        [Test]
        public void SetupInSequenceSingle_ExceptionsOrReturns_Throws_Empty_ExceptionsOrReturns()
        {
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new ExceptionsOrReturns<string>{ }, 1));
        }

        [Test]
        public void SetupInSequenceSingle_Returns()
        {
            var loops = 1;

            var mockedInvocableSequenceReturn = new Mock<IInvocableSequenceReturn<IToMock, string, string>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();

            Returns<string> returns = new Returns<string> { "1" };
            
            mockInvocableFactory.Setup(m => m.CreateSequenceReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, returns)).Returns(mockedInvocableSequenceReturn).Verifiable();

            var wrapped = mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), returns, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceReturn));

        }
        [Test]
        public void SetupInSequenceSingle_Returns_Throws_Loops_Less_Than_One()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new Returns<string> { "1"}, 0));
        }
        [Test]
        public void SetupInSequenceSingle_Returns_Throws_Null_Returns()
        {
            Assert_Null_Returns_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), (Returns<string>)null, 1));
        }
        [Test]
        public void SetupInSequenceSingle_Returns_Throws_Empty_Returns()
        {
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new Returns<string> { }, 1));
        }

        [Test]
        public void SetupInSequenceCreateShared_ExceptionsOrReturns()
        {
            var loops = 1;

            var mockedInvocableSequenceReturn = new Mock<IInvocableSequenceReturn<IToMock, string, ExceptionOrReturn<string>>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared(loops)).Returns(sequence).Verifiable();
            ExceptionsOrReturns<string> exceptionsOrReturns = new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Exception<string>(new Exception()) };
            
            mockInvocableFactory.Setup(m => m.CreateSequenceReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, exceptionsOrReturns)).Returns(mockedInvocableSequenceReturn).Verifiable();

            var wrapped = mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var createdSequence, exceptionsOrReturns, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceReturn));
            Assert.That(createdSequence, Is.EqualTo(sequence));

        }
        [Test]
        public void SetupInSequenceCreateShared_ExceptionsOrReturns_Throws_Loops_Less_Than_One()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, 0));
        }
        [Test]
        public void SetupInSequenceCreateShared_ExceptionsOrReturns_Throws_Null_ExceptionsOrReturns()
        {
            Assert_Null_ExceptionsOrReturns_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, (ExceptionsOrReturns<string>)null, 1));
        }
        [Test]
        public void SetupInSequenceCreateShared_ExceptionsOrReturns_Throws_Empty_ExceptionsOrReturns()
        {
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""), out var sequence, new ExceptionsOrReturns<string> { }, 1));
        }

        [Test]
        public void SetupInSequenceCreateShared_Returns()
        {
            var loops = 1;

            var mockedInvocableSequenceReturn = new Mock<IInvocableSequenceReturn<IToMock, string, string>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared(loops)).Returns(sequence).Verifiable();

            Returns<string> returns = new Returns<string> { "1" };
            
            mockInvocableFactory.Setup(m => m.CreateSequenceReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, returns)).Returns(mockedInvocableSequenceReturn).Verifiable();

            var wrapped = mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var createdSequence, returns, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceReturn));
            Assert.That(createdSequence, Is.EqualTo(sequence));

        }
        [Test]
        public void SetupInSequenceCreateShared_Returns_Throws_Loops_Less_Than_One()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""), out var sequence, new Returns<string> { "1" }, 0));
        }
        [Test]
        public void SetupInSequenceCreateShared_Returns_Throws_Null_Returns()
        {
            Assert_Null_Returns_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""), out var sequence, (Returns<string>)null, 1));
        }
        [Test]
        public void SetupInSequenceCreateShared_Returns_Throws_Empty_Returns()
        {
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""), out var sequence, new Returns<string> { }, 1));
        }

        [Test]
        public void SetupInSequenceShared_ExceptionsOrReturns()
        {
            var mockedInvocableSequenceReturn = new Mock<IInvocableSequenceReturn<IToMock, string, ExceptionOrReturn<string>>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceReturn)).Verifiable();
            var sequence = mockSequence.Object;
            ExceptionsOrReturns<string> exceptionsOrReturns = new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Exception<string>(new Exception()) };
            
            mockInvocableFactory.Setup(m => m.CreateSequenceReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, exceptionsOrReturns)).Returns(mockedInvocableSequenceReturn).Verifiable();

            var wrapped = mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, exceptionsOrReturns);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceReturn));
        }
        [Test]
        public void SetupInSequenceShared_ExceptionsOrReturns_Throws_Null_ExceptionsOrReturns()
        {
            var sequence = new Sequence(0);
            Assert_Null_ExceptionsOrReturns_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, (ExceptionsOrReturns<string>)null));
        }
        [Test]
        public void SetupInSequenceShared_ExceptionsOrReturns_Throws_Empty_ExceptionsOrReturns()
        {
            var sequence = new Sequence(0);
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, new ExceptionsOrReturns<string> { }));
        }
        [Test]
        public void SetupInSequenceShared_ExceptionsOrReturns_Throws_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), null, new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }));
        }


        [Test]
        public void SetupInSequenceShared_Returns()
        {
            var mockedInvocableSequenceReturn = new Mock<IInvocableSequenceReturn<IToMock, string, string>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceReturn)).Verifiable();
            var sequence = mockSequence.Object;

            Returns<string> returns = new Returns<string> { "1" };
            
            mockInvocableFactory.Setup(m => m.CreateSequenceReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, returns)).Returns(mockedInvocableSequenceReturn).Verifiable();

            var wrapped = mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, returns);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceReturn));
        }
        [Test]
        public void SetupInSequenceShared_Returns_Throws_Null_Returns()
        {
            var sequence = new Sequence(0);
            Assert_Null_Returns_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, (Returns<string>)null));
        }
        [Test]
        public void SetupInSequenceShared_Returns_Throws_Empty_Returns()
        {
            var sequence = new Sequence(0);
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, new Returns<string> { }));
        }
        [Test]
        public void SetupInSequenceShared_Returns_Throws_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), null, new Returns<string> { "1"}));
        }


        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_ExceptionsOrReturns()
        {
            var loops = 1;
            var sequenceInvocationIndices=new SequenceInvocationIndices {1,3 };

            var mockedInvocableSequenceReturn = new Mock<IInvocableSequenceReturn<IToMock, string, ExceptionOrReturn<string>>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceReturn)).Verifiable();
            var sequence = mockSequence.Object;

            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();
            ExceptionsOrReturns<string> exceptionsOrReturns = new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Exception<string>(new Exception()), ExceptionOrReturnFactory.Return("1")};
            
            mockInvocableFactory.Setup(m => m.CreateSequenceReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, exceptionsOrReturns, sequenceInvocationIndices)).Returns(mockedInvocableSequenceReturn).Verifiable();

            var wrapped = mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), exceptionsOrReturns, sequenceInvocationIndices, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceReturn));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_ExceptionsOrReturns_Throws_With_Loops_Less_Than_One()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, new SequenceInvocationIndices { 1 }, 0));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_ExceptionsOrReturns_Throws_With_Null_ExceptionsOrReturns()
        {
            var sequence = new Sequence(0);
            Assert_Null_ExceptionsOrReturns_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), (ExceptionsOrReturns<string>)null, new SequenceInvocationIndices { 1 }, 1));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_ExceptionsOrReturns_Throws_With_Empty_ExceptionsOrReturns()
        {
            var sequence = new Sequence(0);
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new ExceptionsOrReturns<string>(), new SequenceInvocationIndices { 1 }, 1));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_ExceptionsOrReturns_Throws_With_Null_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, (SequenceInvocationIndices)null, 1));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_ExceptionsOrReturns_ThrowsWith__Empty_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, new SequenceInvocationIndices { }));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_ExceptionsOrReturns_Throws_Incorrect_Number_Of_Responses()
        {
            var sequence = new Sequence(0);
            Assert_Incorrect_Number_Of_ConfiguredResponses_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, new SequenceInvocationIndices { 1, 2 }));
        }

        
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_Returns()
        {
            var loops = 1;
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1, 3 };

            var mockedInvocableSequenceReturn = new Mock<IInvocableSequenceReturn<IToMock, string, string>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateSingle(mock, loops)).Returns(sequence).Verifiable();

            Returns<string> returns = new Returns<string> { "1","2" };
            
            mockInvocableFactory.Setup(m => m.CreateSequenceReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, returns, sequenceInvocationIndices)).Returns(mockedInvocableSequenceReturn).Verifiable();

            var wrapped = mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), returns, sequenceInvocationIndices, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceReturn));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_Returns_Throws_With_Loops_Less_Than_One()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new Returns<string> { "1"}, new SequenceInvocationIndices { 1 }, 0));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_Returns_Throws_With_Null_Returns()
        {
            var sequence = new Sequence(0);
            Assert_Null_Returns_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), (Returns<string>)null, new SequenceInvocationIndices { 1 }, 1));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_Returns_Throws_With_Empty_Returns()
        {
            var sequence = new Sequence(0);
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new Returns<string>(), new SequenceInvocationIndices { 1 }, 1));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_Returns_Throws_With_Null_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new Returns<string> { "1" }, (SequenceInvocationIndices)null, 1));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_Returns_ThrowsWith__Empty_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new Returns<string> { "1" }, new SequenceInvocationIndices { }));
        }
        [Test]
        public void SetupInSequenceSingle_SequenceInvocationIndices_Returns_Throws_Incorrect_Number_Of_Responses()
        {
            var sequence = new Sequence(0);
            Assert_Incorrect_Number_Of_ConfiguredResponses_Throws(() => mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new Returns<string> { "1" }, new SequenceInvocationIndices { 1, 2 }));
        }

        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_ExceptionsOrReturns()
        {
            var loops = 1;
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1, 3 };

            var mockedInvocableSequenceReturn = new Mock<IInvocableSequenceReturn<IToMock, string, ExceptionOrReturn<string>>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceReturn)).Verifiable();
            var sequence = mockSequence.Object;

            mockSequenceFactory.Setup(m => m.CreateShared(loops)).Returns(sequence).Verifiable();
            ExceptionsOrReturns<string> exceptionsOrReturns = new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Exception<string>(new Exception()), ExceptionOrReturnFactory.Return("1") };
            
            mockInvocableFactory.Setup(m => m.CreateSequenceReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, exceptionsOrReturns, sequenceInvocationIndices)).Returns(mockedInvocableSequenceReturn).Verifiable();

            var wrapped = mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var createdSequence, exceptionsOrReturns, sequenceInvocationIndices, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceReturn));
            Assert.That(createdSequence, Is.EqualTo(sequence));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_ExceptionsOrReturns_Throws_With_Loops_Less_Than_One()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, new SequenceInvocationIndices { 1 }, 0));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_ExceptionsOrReturns_Throws_With_Null_ExceptionsOrReturns()
        {
            Assert_Null_ExceptionsOrReturns_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, (ExceptionsOrReturns<string>)null, new SequenceInvocationIndices { 1 }, 1));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_ExceptionsOrReturns_Throws_With_Empty_ExceptionsOrReturns()
        {
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, new ExceptionsOrReturns<string>(), new SequenceInvocationIndices { 1 }, 1));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_ExceptionsOrReturns_Throws_With_Null_SequenceInvocationIndices()
        {
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, (SequenceInvocationIndices)null, 1));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_ExceptionsOrReturns_ThrowsWith__Empty_SequenceInvocationIndices()
        {
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, new SequenceInvocationIndices { }));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_ExceptionsOrReturns_Throws_Incorrect_Number_Of_Responses()
        {
            Assert_Incorrect_Number_Of_ConfiguredResponses_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, new SequenceInvocationIndices { 1, 2 }));
        }


        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_Returns()
        {
            var loops = 1;
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1, 3 };

            var mockedInvocableSequenceReturn = new Mock<IInvocableSequenceReturn<IToMock, string, string>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceReturn)).Verifiable();
            var sequence = mockSequence.Object;
            mockSequenceFactory.Setup(m => m.CreateShared( loops)).Returns(sequence).Verifiable();
            Returns<string> returns = new Returns<string> { "1", "2" };
            
            mockInvocableFactory.Setup(m => m.CreateSequenceReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, returns, sequenceInvocationIndices)).Returns(mockedInvocableSequenceReturn).Verifiable();

            var wrapped = mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var createdSequence, returns, sequenceInvocationIndices, loops);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceReturn));
            Assert.That(createdSequence, Is.EqualTo(sequence));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_Returns_Throws_With_Loops_Less_Than_One()
        {
            Assert_Loops_Less_Than_1_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, new Returns<string> { "1" }, new SequenceInvocationIndices { 1 }, 0));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_Returns_Throws_With_Null_Returns()
        {
            Assert_Null_Returns_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, (Returns<string>)null, new SequenceInvocationIndices { 1 }, 1));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_Returns_Throws_With_Empty_Returns()
        {
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, new Returns<string>(), new SequenceInvocationIndices { 1 }, 1));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_Returns_Throws_With_Null_SequenceInvocationIndices()
        {
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, new Returns<string> { "1" }, (SequenceInvocationIndices)null, 1));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_Returns_ThrowsWith__Empty_SequenceInvocationIndices()
        {
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, new Returns<string> { "1" }, new SequenceInvocationIndices { }));
        }
        [Test]
        public void SetupInSequenceCreateShared_SequenceInvocationIndices_Returns_Throws_Incorrect_Number_Of_Responses()
        {
            Assert_Incorrect_Number_Of_ConfiguredResponses_Throws(() => mock.SetupInSequenceCreateShared(m => m.MethodWithArgument(""),out var sequence, new Returns<string> { "1" }, new SequenceInvocationIndices { 1, 2 }));
        }


        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_ExceptionsOrReturns()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1, 3 };

            var mockedInvocableSequenceReturn = new Mock<IInvocableSequenceReturn<IToMock, string, ExceptionOrReturn<string>>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceReturn)).Verifiable();
            var sequence = mockSequence.Object;

            ExceptionsOrReturns<string> exceptionsOrReturns = new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Exception<string>(new Exception()), ExceptionOrReturnFactory.Return("1") };
            
            mockInvocableFactory.Setup(m => m.CreateSequenceReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, exceptionsOrReturns, sequenceInvocationIndices)).Returns(mockedInvocableSequenceReturn).Verifiable();

            var wrapped = mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, exceptionsOrReturns, sequenceInvocationIndices);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceReturn));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_ExceptionsOrReturns_Throws_With_Null_ExceptionsOrReturns()
        {
            var sequence = new Sequence(0);
            Assert_Null_ExceptionsOrReturns_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, (ExceptionsOrReturns<string>)null, new SequenceInvocationIndices { 1 }));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_ExceptionsOrReturns_Throws_With_Empty_ExceptionsOrReturns()
        {
            var sequence = new Sequence(0);
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, new ExceptionsOrReturns<string>(), new SequenceInvocationIndices { 1 }));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_ExceptionsOrReturns_Throws_With_Null_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""),sequence, new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, (SequenceInvocationIndices)null));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_ExceptionsOrReturns_ThrowsWith__Empty_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, new SequenceInvocationIndices { }));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_ExceptionsOrReturns_Throws_Incorrect_Number_Of_Responses()
        {
            var sequence = new Sequence(0);
            Assert_Incorrect_Number_Of_ConfiguredResponses_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, new SequenceInvocationIndices { 1, 2 }));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_ExceptionsOrReturns_Throws_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), null, new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1") }, new SequenceInvocationIndices { 1 }));
        }


        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_Returns()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1, 3 };

            var mockedInvocableSequenceReturn = new Mock<IInvocableSequenceReturn<IToMock, string, string>>().Object;

            var mockSequence = mockRepository.Create<ISequence>();
            mockSequence.Setup(m => m.RegisterInvocable(mockedInvocableSequenceReturn)).Verifiable();
            var sequence = mockSequence.Object;

            Returns<string> returns = new Returns<string> { "1", "2" };
            
            mockInvocableFactory.Setup(m => m.CreateSequenceReturn(It.IsNotNull<ISetup<IToMock, string>>(), mock, sequence, returns, sequenceInvocationIndices)).Returns(mockedInvocableSequenceReturn).Verifiable();

            var wrapped = mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, returns, sequenceInvocationIndices);

            mockRepository.Verify();
            Assert.That(wrapped, Is.EqualTo(mockedInvocableSequenceReturn));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_Returns_Throws_With_Null_Returns()
        {
            var sequence = new Sequence(0);
            Assert_Null_Returns_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, (Returns<string>)null, new SequenceInvocationIndices { 1 }));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_Returns_Throws_With_Empty_Returns()
        {
            var sequence = new Sequence(0);
            Assert_Empty_Responses_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""),sequence, new Returns<string>(), new SequenceInvocationIndices { 1 }));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_Returns_Throws_With_Null_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Null_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, new Returns<string> { "1" }, (SequenceInvocationIndices)null));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_Returns_ThrowsWith__Empty_SequenceInvocationIndices()
        {
            var sequence = new Sequence(0);
            Assert_Empty_SequenceInvocationIndices_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, new Returns<string> { "1" }, new SequenceInvocationIndices { }));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_Returns_Throws_Incorrect_Number_Of_Responses()
        {
            var sequence = new Sequence(0);
            Assert_Incorrect_Number_Of_ConfiguredResponses_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), sequence, new Returns<string> { "1" }, new SequenceInvocationIndices { 1, 2 }));
        }
        [Test]
        public void SetupInSequenceShared_SequenceInvocationIndices_Returns_Throws_Null_Sequence()
        {
            Assert_Null_Sequence_Throws(() => mock.SetupInSequenceShared(m => m.MethodWithArgument(""), null, new Returns<string> { "1" }, new SequenceInvocationIndices { 1 }));
        }


        #endregion
        #endregion
    }

    public static class SequenceExceptionAssertions
    {
        public static void Throws_Insufficient_Calls(Action action, int call, int loop = 1)
        {
            Assert.That(
                ThrewHelper.Threw<SequenceVerifyException>(action, exc =>
                {
                    return exc.InsufficientCalls && exc.Call == call && exc.Loop == loop;
                })
            );
        }
        public static void Throws_Insufficient_Calls(ISequence sequence, int call, int loop = 1)
        {
            Throws_Insufficient_Calls(() => sequence.Verify(), call, loop);
        }
        public static void Throws_Insufficient_Calls<T>(Mock<T> mock, int call, int loop = 1) where T:class
        {
            Throws_Insufficient_Calls(()=>mock.VerifySequence(),call,loop);
        }
        public static void Throws_Out_Of_Sequence(ISequence sequence, int call, int loop = 1)
        {
            Throws_Out_Of_Sequence(() => sequence.Verify(), call, loop);
        }
        public static void Throws_Out_Of_Sequence(Action action, int call, int loop = 1)
        {
            Assert.That(
                ThrewHelper.Threw<SequenceVerifyException>(action, exc =>
                {
                    return exc.OutOfSequence && exc.Call == call && exc.Loop == loop;
                })
            );
        }
        public static void Throws_Out_Of_Sequence<T>(Mock<T> mock, int call, int loop = 1) where T : class
        {
            Throws_Out_Of_Sequence(() => mock.VerifySequence(), call, loop);
        }
        public static void Throws_Additional_Calls(Action action, int expectedCount, int actualCount)
        {
            Assert.That(
                ThrewHelper.Threw<SequenceVerifyException>(action, exc =>
                {
                    return exc.TooManyCalls && exc.ExpectedCount == expectedCount && exc.ActualCount == actualCount;
                })
            );
        }
        public static void Throws_Additional_Calls<T>(Mock<T> mock, int expectedCount, int actualCount) where T:class
        {
            Throws_Additional_Calls(() => mock.VerifySequence(), expectedCount, actualCount);
        }
        public static void Throws_Additional_Calls(ISequence sequence, int expectedCount, int actualCount)
        {
            Throws_Additional_Calls(() => sequence.Verify(), expectedCount, actualCount);
        }
    }

    

    [TestFixture]
    [Ignore("refactoring tests")]
    public class InSequence_General_Tests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Throws_When_Mix_Times_And_Indices(bool timesFirst)
        {
            var mock = new Mock<IToMock>();
            Action throwAction = null;
            if (timesFirst)
            {
                throwAction = () =>
                {
                    mock.SetupInSequenceSingle(m => m.MethodWithArgument("1"), 1);
                    mock.SetupInSequenceSingle(m => m.MethodWithArgument("1"), new SequenceInvocationIndices { 1, 2 });
                };
            }
            else
            {
                throwAction = () =>
                {
                    mock.SetupInSequenceSingle(m => m.MethodWithArgument("1"), new SequenceInvocationIndices { 1, 2 });
                    mock.SetupInSequenceSingle(m => m.MethodWithArgument("1"), 1);
                };
            }

            Assert.That(
                ThrewHelper.Threw<SequenceSetupException>(throwAction, (e => e.MixedTimesAndCallIndices))
            );
        }
        [Test]
        public void Throws_When_Different_Setups_Supply_The_Same_Call_Index()
        {
            var mock = new Mock<IToMock>();
            mock.SetupInSequenceSingle(m => m.Method1(), new SequenceInvocationIndices { 1 });
            Assert.That(ThrewHelper.Threw<SequenceSetupException>(() =>
            {
                mock.SetupInSequenceSingle(m => m.Method2(), new SequenceInvocationIndices { 1 });
            }, (e) =>
            {
                return e.RepeatedCallIndexAcrossSetups;
            }));

        }
        [Test]
        public void Works_With_Callbacks_From_Outside()
        {
            var mock = new Mock<IToMock>();
            var propertyCallbackCalled = false;
            mock.SetupGetInSequenceSingle(m => m.Prop).Callback(() =>
            {
                propertyCallbackCalled = true;
            });
            string callbackArg = null;
            var setUp = mock.SetupInSequenceSingle(m => m.MethodWithArgument(It.IsAny<string>()));
            setUp.Callback<string>(a =>
            {

            });
            //demonstrating the usual behaviour of last callback wins
            setUp.Callback<string>(a =>
            {
                callbackArg = a;
            });
            var mocked = mock.Object;

            var p = mocked.Prop;

            var methodArg = "Arg";
            mocked.MethodWithArgument(methodArg);

            mock.VerifySequence();
            Assert.That(propertyCallbackCalled);
            Assert.That(callbackArg, Is.EqualTo(methodArg));
        }
    }

    [TestFixture]
    [Ignore("refactoring tests")]
    class InSequence_Single_Mock_1_Loop_Tests
    {
        [Test]
        public void SingleSequence_Verify_Times_Should_Not_Throw_When_In_Sequence()
        {
            var mock = new Mock<IToMock>();
            mock.SetupGetInSequenceSingle(m => m.Prop);
            mock.SetupInSequenceSingle(m => m.MethodWithArgument(It.IsAny<string>()),2);
            var mocked = mock.Object;
            
            var p = mocked.Prop;
            mocked.MethodWithArgument("");
            mocked.MethodWithArgument("");

            Assert.That(() => mock.VerifySequence(), Throws.Nothing);
        }
        [Test]
        public void SingleSequenceVerify_CallIndices_Should_Not_Throw_When_In_Sequence()
        {
            var mock = new Mock<IToMock>();
            mock.SetupInSequenceSingle(m => m.Method1(), new SequenceInvocationIndices { 0, 2 });
            mock.SetupInSequenceSingle(m => m.Method2(), new SequenceInvocationIndices { 1 });

            var mocked = mock.Object;
            mocked.Method1();
            mocked.Method2();
            mocked.Method1();

            Assert.That(()=>mock.VerifySequence(), Throws.Nothing);
        }

        [Test]
        public void SingleSequenceVerify_Times_Should_Throw_When_Insufficient_Calls()
        {
            var mock = new Mock<IToMock>();
            mock.SetupInSequenceSingle(m => m.Method1(), 1);
            mock.SetupInSequenceSingle(m => m.MethodWithArgument("Match"), 2);

            var mocked = mock.Object;
            
            mocked.Method1();
            mocked.MethodWithArgument("Match");
            mocked.MethodWithArgument("No match");

            SequenceExceptionAssertions.Throws_Insufficient_Calls(mock,2,1);
        }
        [Test]
        public void SingleSequenceVerify_CallIndices_Should_Throw_When_Insufficient_Calls()
        {
            var mock = new Mock<IToMock>();
            mock.SetupInSequenceSingle(m => m.Method1(), SequenceInvocationIndices.Singular(1));
            mock.SetupInSequenceSingle(m => m.MethodWithArgument("Match"), new SequenceInvocationIndices { 0, 2 });

            var mocked = mock.Object;
            mocked.MethodWithArgument("Match");
            mocked.Method1();
            mocked.MethodWithArgument("No match");

            SequenceExceptionAssertions.Throws_Insufficient_Calls(mock,2,1);
        }

        [Test]
        public void SingleSequenceVerify_Times_Should_Throw_When_Additional_Calls()
        {
            var mock = new Mock<IToMock>();
            mock.SetupInSequenceSingle(m => m.Method1(), 1);

            var mocked = mock.Object;

            mocked.Method1();
            mocked.Method1();
            SequenceExceptionAssertions.Throws_Additional_Calls(mock, 1, 2);
        }
        [Test]
        public void SingleSequenceVerify_CallIndices_Should_Throw_When_Additional_Calls()
        {
            var mock = new Mock<IToMock>();
            mock.SetupInSequenceSingle(m => m.Method1(), SequenceInvocationIndices.Singular(1));
            mock.SetupInSequenceSingle(m => m.MethodWithArgument("Match"), new SequenceInvocationIndices { 0, 2 });

            var mocked = mock.Object;
            mocked.MethodWithArgument("Match");
            mocked.Method1();
            mocked.MethodWithArgument("Match");
            mocked.Method1();

            SequenceExceptionAssertions.Throws_Additional_Calls(mock, 3, 4);
        }
        
        [Test]
        public void SingleSequenceVerify_CallIndices_Should_Throw_When_Out_Of_Sequence()
        {
            var mock = new Mock<IToMock>();
            mock.SetupInSequenceSingle(m => m.Method1(), SequenceInvocationIndices.Singular(1));
            mock.SetupInSequenceSingle(m => m.MethodWithArgument("Match"), new SequenceInvocationIndices { 0, 2 });

            var mocked = mock.Object;
            mocked.MethodWithArgument("Match");
            mocked.Method1();
            mocked.Method1();

            SequenceExceptionAssertions.Throws_Out_Of_Sequence(mock, 2);
        }
        [Test]
        public void SingleSequenceVerify_Times_Should_Throw_When_Out_Of_Sequence()
        {
            var mock = new Mock<IToMock>();
            mock.SetupInSequenceSingle(m => m.Method1(), 1);
            mock.SetupInSequenceSingle(m => m.MethodWithArgument("Match"),2);

            var mocked = mock.Object;
            
            mocked.Method1();
            mocked.MethodWithArgument("Match");
            mocked.Method1();

            SequenceExceptionAssertions.Throws_Out_Of_Sequence(mock, 2);
        }


        [Test]
        public void Adds_The_Fake_Invocation()
        {
            var mock = new Mock<IToMock>(MockBehavior.Strict);
            mock.SetupGetInSequenceSingle(m => m.Prop);
            Assert.That(mock.Invocations.Count > 0);
            Assert.That(mock.Invocations.WithoutAdditional().Count() == 0);

        }
        [Test]//****************************************  This is when need to go to indices
        public void Fails_When_Setup_The_Same_Again()
        {
            var mock = new Mock<IToMock>();
            mock.SetupGetInSequenceSingle(m => m.Prop);
            mock.SetupGetInSequenceSingle(m => m.Prop);
            var mocked = mock.Object;
            var _ = mocked.Prop;
            _ = mocked.Prop;
            Assert.That(() => mock.VerifySequence(), Throws.InstanceOf<SequenceException>());

        }


        [TestCase(true, MockBehavior.Loose, ExpectedResult = false)]
        [TestCase(false, MockBehavior.Loose, ExpectedResult = true)]
        public bool SingleSequence_Verify_Works_With_Multiple_Times(bool twice, MockBehavior mockBehavior)
        {
            var mock = new Mock<IToMock>();
            mock.SetupGetInSequenceSingle(m => m.Prop, 2);

            var mocked = mock.Object;
            if (twice)
            {
                var p = mocked.Prop;
                var p2 = mocked.Prop;
            }
            else
            {
                var p = mocked.Prop;
            }
            return ThrewHelper.Threw<SequenceVerifyException>(() => mock.VerifySequence(), e => e.InsufficientCalls);

        }
        [Test]
        public void SingleSequenceVerify_Delegate_Times_Receives_Expected_Arguments()
        {
            var mock = new Mock<IToMock>();
            var setupGet=mock.SetupGetInSequenceSingle(m => m.Prop);
            var setupSet=mock.SetupSetInSequenceSingle(m => m.Prop = 2, 2);

            var mocked = mock.Object;
            mocked.Method1();//not in sequence
            mocked.Prop = 3;//not in sequence
            mocked.Prop = 2;
            var _=mocked.Prop;
            bool callbackCalled = false;
            mock.VerifySequence((expectedCalls, calls) =>
            {
                callbackCalled = true;
                Assert.That(expectedCalls, Is.EquivalentTo(new object[] { setupGet, setupSet, setupSet }));
                Assert.That(calls, Is.EquivalentTo(new object[] { setupSet, setupGet }));

            });
            Assert.That(callbackCalled);
        }
        [Test]
        public void SingleSequenceVerify_Delegate_CallIndices_Receives_Expected_Arguments()
        {
            var mock = new Mock<IToMock>();
            var setupGet = mock.SetupGetInSequenceSingle(m => m.Prop,new SequenceInvocationIndices { 0,2});
            var setupSet = mock.SetupSetInSequenceSingle(m => m.Prop = 2, SequenceInvocationIndices.Singular(1));

            bool callbackCalled = false;
            mock.VerifySequence((expectedCalls, calls) =>
            {
                callbackCalled = true;
                Assert.That(expectedCalls, Is.EquivalentTo(new object[] { setupGet, setupSet, setupGet }));
            });
            Assert.That(callbackCalled);
        }
    }

    [TestFixture]
    [Ignore("refactoring tests")]
    class In_Sequence_Multiple_Mocks_1_Loop_Tests
    {
        private (IToMock mocked1,IOtherToMock mocked2,ISequence sequence) SetUpTimes()
        {
            var mock1 = new Mock<IToMock>();
            mock1.SetupInSequenceCreateShared(m => m.MethodWithArgument(It.IsAny<string>()), out var sequence);
            var mock2 = new Mock<IOtherToMock>();
            mock2.SetupGetInSequenceShared(m => m.SomeProp, sequence, 2);
            return (mock1.Object, mock2.Object,sequence);
        }
        private (IToMock mocked1, IOtherToMock mocked2, ISequence sequence) SetUpCallIndices()
        {
            var mock1 = new Mock<IToMock>();
            mock1.SetupInSequenceCreateShared(m => m.MethodWithArgument(It.IsAny<string>()), out var sequence,SequenceInvocationIndices.Singular(0));
            var mock2 = new Mock<IOtherToMock>();
            mock2.SetupGetInSequenceShared(m => m.SomeProp, sequence, new SequenceInvocationIndices { 1,2});
            return (mock1.Object, mock2.Object, sequence);
        }

        [Test]
        public void Sequence_Verify_Times_Should_Not_Throw_When_In_Sequence()
        {
            var (mocked1, mocked2,sequence) = SetUpTimes();
            
            mocked1.MethodWithArgument("");
            var _ = mocked2.SomeProp;
            _ = mocked2.SomeProp;

            Assert.That(() => sequence.Verify(), Throws.Nothing);
        }
        [Test]
        public void Sequence_Verify_CallIndices_Should_Not_Throw_When_In_Sequence()
        {
            var (mocked1, mocked2, sequence) = SetUpCallIndices();

            mocked1.MethodWithArgument("");
            var _ = mocked2.SomeProp;
            _ = mocked2.SomeProp;

            Assert.That(() => sequence.Verify(), Throws.Nothing);
        }
        
        [Test]
        public void Sequence_Verify_Times_Should_Throw_When_Insufficient_Calls()
        {
            var (mocked1, mocked2, sequence) = SetUpTimes();

            mocked1.MethodWithArgument("");
            var _ = mocked2.SomeProp;
            
            SequenceExceptionAssertions.Throws_Insufficient_Calls(sequence,2);
        }
        [Test]
        public void Sequence_Verify_CallIndices_Should_Throw_When_Insufficient_Calls()
        {
            var (mocked1, mocked2, sequence) = SetUpCallIndices();

            mocked1.MethodWithArgument("");
            var _ = mocked2.SomeProp;

            SequenceExceptionAssertions.Throws_Insufficient_Calls(sequence,2);
        }
        
        [Test]
        public void Sequence_Verify_Times_Should_Throw_When_Out_Of_Sequence()
        {
            var (mocked1, mocked2, sequence) = SetUpTimes();

            mocked1.MethodWithArgument("");
            var _ = mocked2.SomeProp;
            mocked1.MethodWithArgument("");

            SequenceExceptionAssertions.Throws_Out_Of_Sequence(sequence, 2);
        }
        [Test]
        public void Sequence_Verify_CallIndices_Should_Throw_When_Out_Of_Sequence()
        {
            var (mocked1, mocked2, sequence) = SetUpCallIndices();

            mocked1.MethodWithArgument("");
            var _ = mocked2.SomeProp;
            mocked1.MethodWithArgument("");

            SequenceExceptionAssertions.Throws_Out_Of_Sequence(sequence, 2);
        }

        [Test]
        public void Sequence_Verify_Times_Should_Throw_When_Too_Many_Calls()
        {
            var (mocked1, mocked2, sequence) = SetUpTimes();
            mocked1.MethodWithArgument("");
            var _ = mocked2.SomeProp;
            _ = mocked2.SomeProp;
            _ = mocked2.SomeProp;
            _ = mocked2.SomeProp;

            SequenceExceptionAssertions.Throws_Additional_Calls(sequence, 3, 5);
        }
        [Test]
        public void Sequence_Verify_CallIndices_Should_Throw_When_Too_Many_Calls()
        {
            var (mocked1, mocked2, sequence) = SetUpCallIndices();

            mocked1.MethodWithArgument("");
            var _ = mocked2.SomeProp;
            _ = mocked2.SomeProp;
            _ = mocked2.SomeProp;

            SequenceExceptionAssertions.Throws_Additional_Calls(sequence, 3, 4); 
        }


        [Test]
        public void SequenceVerify_Delegate_Times_Receives_Expected_Arguments()
        {
            var mock1 = new Mock<IToMock>();
            var mock2 = new Mock<IOtherToMock>();

            var setup1=mock1.SetupInSequenceCreateShared(m => m.Method1(), out var sequence);
            var setup2=mock2.SetupGetInSequenceShared(m => m.SomeProp, sequence, 2);
            var setup3=mock1.SetupInSequenceShared(m => m.Method2(), sequence, 3);

            var mocked1 = mock1.Object;
            var mocked2 = mock2.Object;

            var _ = mocked2.SomeProp;
            mocked1.Method2();
            _ = mocked2.SomeProp;
            mocked1.Method1();

            bool callbackCalled = false;
            sequence.Verify(( expectedCalls, calls) =>
            {
                Assert.That(expectedCalls, Is.EquivalentTo(new object[] { setup1, setup2, setup2, setup3, setup3, setup3 }));
                Assert.That(calls, Is.EquivalentTo(new object[] { setup2, setup3, setup2, setup1 }));
                callbackCalled = true;
            });
            Assert.That(callbackCalled);
        }
        [Test]
        public void SequenceVerify_Delegate_CallIndices_Receives_Expected_Arguments()
        {
            var mock1 = new Mock<IToMock>();
            var mock2 = new Mock<IOtherToMock>();

            var setup1 = mock1.SetupInSequenceCreateShared(m => m.Method1(), out var sequence,new SequenceInvocationIndices { 0,2});
            var setup2 = mock2.SetupGetInSequenceShared(m => m.SomeProp, sequence, SequenceInvocationIndices.Singular(3));
            var setup3 = mock1.SetupInSequenceShared(m => m.Method2(), sequence, new SequenceInvocationIndices { 1,4});

            bool callbackCalled = false;
            sequence.Verify((expectedCalls, calls) =>
            {
                Assert.That(expectedCalls, Is.EquivalentTo(new object[] { setup1, setup3, setup1, setup2,setup3 }));
                callbackCalled = true;
            });
            Assert.That(callbackCalled);
        }

        [Test]
        public void Shared_Has_No_Fake_Invocations()
        {
            var mock = new Mock<IToMock>(MockBehavior.Strict);
            mock.SetupGetInSequenceCreateShared(m => m.Prop,out var sequence);
            Assert.That(mock.Invocations.Count == 0);
        }
    }

    [TestFixture]
    [Ignore("refactoring tests")]
    class In_Sequence_Multiple_Mocks_Multiple_Loops
    {
        private (IToMock mocked1, IOtherToMock mocked2, ISequence sequence) SetUpCallIndices()
        {
            var mock1 = new Mock<IToMock>();
            var mock2 = new Mock<IOtherToMock>();
            mock1.SetupInSequenceCreateShared(m => m.Method1(), out var sequence, new SequenceInvocationIndices { 0, 2 }, 2);
            mock2.SetupGetInSequenceShared(m => m.SomeProp, sequence, SequenceInvocationIndices.Singular(1));
            return (mock1.Object, mock2.Object, sequence);
        }

        [Test]
        public void Sequence_Verify_Should_Not_Throw_If_In_Sequence()
        {
            var (mocked1, mocked2, sequence) = SetUpCallIndices();

            mocked1.Method1();
            var _ = mocked2.SomeProp;
            mocked1.Method1();

            mocked1.Method1();
            _ = mocked2.SomeProp;
            mocked1.Method1();

            Assert.That(() => sequence.Verify(), Throws.Nothing);
        }
        [Test]
        public void Sequence_Verify_Should_Throw_When_Too_Many_Calls()
        {
            var (mocked1, mocked2, sequence) = SetUpCallIndices();

            mocked1.Method1();
            var _ = mocked2.SomeProp;
            mocked1.Method1();

            mocked1.Method1();
            _ = mocked2.SomeProp;
            mocked1.Method1();

            mocked1.Method1();
            SequenceExceptionAssertions.Throws_Additional_Calls(sequence, 6, 7);
        }
        [Test]
        public void Sequence_Verify_Should_Throw_On_The_First_Loop_When_Out_Of_Sequence()
        {
            var (mocked1, mocked2, sequence) = SetUpCallIndices();

            mocked1.Method1();
            mocked1.Method1();

            SequenceExceptionAssertions.Throws_Out_Of_Sequence(sequence,1,1);
        }
        [Test]
        public void Sequence_Verify_Should_Throw_On_The_Second_Loop_When_Out_Of_Sequence()
        {
            var (mocked1, mocked2, sequence) = SetUpCallIndices();

            mocked1.Method1();
            var _ = mocked2.SomeProp;
            mocked1.Method1();

            _ = mocked2.SomeProp;

            SequenceExceptionAssertions.Throws_Out_Of_Sequence(sequence, 0, 2);
        }
        [Test]
        public void Sequence_Verify_Should_Throw_On_The_First_Loop_When_Insufficient_Calls()
        {
            var (mocked1, mocked2, sequence) = SetUpCallIndices();

            mocked1.Method1();
            var _ = mocked2.SomeProp;

            SequenceExceptionAssertions.Throws_Insufficient_Calls(sequence,2,1);
        }
        [Test]
        public void Sequence_Verify_Should_Throw_On_The_Second_Loop_When_Insufficient_Calls()
        {
            var (mocked1, mocked2, sequence) = SetUpCallIndices();

            mocked1.Method1();
            var _ = mocked2.SomeProp;
            mocked1.Method1();

            mocked1.Method1();
            
            SequenceExceptionAssertions.Throws_Insufficient_Calls(sequence,1,2);
        }
        [Test]
        public void SequenceVerify_Delegate_CallIndices_Receives_Expected_Arguments()
        {
            var mock1 = new Mock<IToMock>();
            var mock2 = new Mock<IOtherToMock>();

            var setup1 = mock1.SetupInSequenceCreateShared(m => m.Method1(), out var sequence, new SequenceInvocationIndices { 0, 2 },2);
            var setup2 = mock2.SetupGetInSequenceShared(m => m.SomeProp, sequence, SequenceInvocationIndices.Singular(3));
            var setup3 = mock1.SetupInSequenceShared(m => m.Method2(), sequence, new SequenceInvocationIndices { 1, 4 });

            bool callbackCalled = false;
            sequence.Verify((expectedCalls, calls) =>
            {
                Assert.That(expectedCalls, Is.EquivalentTo(new object[] { setup1, setup3, setup1, setup2, setup3, setup1, setup3, setup1, setup2, setup3 }));
                callbackCalled = true;
            });
            Assert.That(callbackCalled);
        }
    }


    [TestFixture]
    [Ignore("refactoring tests")]
    public class InSequence_Verify_Upon_Call
    {
        [Test]
        public void Should_Throw_If_Out_Of_Sequence_From_The_Start()
        {
            Sequences.VerifyUponInvocation = VerifyUponInvocation.Yes;
            var mock = new Mock<IToMock>();
            mock.SetupGetInSequenceSingle(m => m.Prop);
            mock.SetupInSequenceSingle(m => m.Method1());

            var mocked = mock.Object;
            SequenceExceptionAssertions.Throws_Out_Of_Sequence(() => mocked.Method1(), 0, 1);
        }
        [Test]
        public void Should_Throw_If_Out_Of_Sequence_After_The_Start()
        {
            Sequences.VerifyUponInvocation = VerifyUponInvocation.Yes;
            var mock = new Mock<IToMock>();
            mock.SetupGetInSequenceSingle(m => m.Prop, 2);
            mock.SetupInSequenceSingle(m => m.Method1());

            var mocked = mock.Object;
            var _ = mocked.Prop;
            SequenceExceptionAssertions.Throws_Out_Of_Sequence(() => mocked.Method1(), 1, 1);
        }
        [Test]
        public void Should_Throw_If_Out_Of_Sequence_With_Loops()
        {
            Sequences.VerifyUponInvocation = VerifyUponInvocation.Yes;
            var mock = new Mock<IToMock>();
            mock.SetupGetInSequenceSingle(m => m.Prop, new SequenceInvocationIndices { 0, 2 }, 2);
            mock.SetupInSequenceSingle(m => m.Method1(), SequenceInvocationIndices.Singular(1));

            var mocked = mock.Object;
            var _ = mocked.Prop;
            mocked.Method1();
            _ = mocked.Prop;

            SequenceExceptionAssertions.Throws_Out_Of_Sequence(() => mocked.Method1(), 0, 2);
        }
        [Test]
        public void Should_Throw_If_Insufficient_Calls_From_The_Start()
        {
            Sequences.VerifyUponInvocation = VerifyUponInvocation.Yes;
            var mock1 = new Mock<IToMock>();
            var mock2 = new Mock<IOtherToMock>();

            mock1.SetupInSequenceCreateShared(m => m.Method1(), out var sequence, 1);
            SequenceExceptionAssertions.Throws_Insufficient_Calls(sequence, 0,1);
        }
        [Test]
        public void Should_Throw_If_Insufficient_Call_After_The_Start()
        {
            Sequences.VerifyUponInvocation = VerifyUponInvocation.Yes;
            var mock1 = new Mock<IToMock>();
            var mock2 = new Mock<IOtherToMock>();

            mock1.SetupInSequenceCreateShared(m => m.Method1(), out var sequence, 1);
            mock2.SetupInSequenceShared(m => m.Method1(), sequence, 2);

            mock1.Object.Method1();
            mock2.Object.Method1();
            SequenceExceptionAssertions.Throws_Insufficient_Calls(sequence, 2, 1);
        }
        [Test]
        public void Should_Throw_If_Insufficient_Call_With_Loops()
        {
            Sequences.VerifyUponInvocation = VerifyUponInvocation.Yes;
            var mock1 = new Mock<IToMock>();
            var mock2 = new Mock<IOtherToMock>();

            mock1.SetupInSequenceCreateShared(m => m.Method1(), out var sequence, 1, 2);
            mock2.SetupInSequenceShared(m => m.Method1(), sequence, 2);

            mock1.Object.Method1();
            mock2.Object.Method1();
            mock2.Object.Method1();

            SequenceExceptionAssertions.Throws_Insufficient_Calls(sequence, 0, 2);
        }
        [Test]
        public void Should_Not_Throw_If_In_Sequence()
        {
            Sequences.VerifyUponInvocation = VerifyUponInvocation.Yes;
            var mock1 = new Mock<IToMock>();
            var mock2 = new Mock<IOtherToMock>();

            mock1.SetupInSequenceCreateShared(m => m.Method1(), out var sequence, 1, 2);
            mock2.SetupInSequenceShared(m => m.Method1(), sequence, 2);

            Assert.That(() =>
            {
                mock1.Object.Method1();
                mock2.Object.Method1();
                mock2.Object.Method1();

                mock1.Object.Method1();
                mock2.Object.Method1();
                mock2.Object.Method1();

            }, Throws.Nothing);
            Assert.That(() => sequence.Verify(), Throws.Nothing);
        }
        [Test]
        public void Should_Throw_If_Too_Many_Calls()
        {
            Sequences.VerifyUponInvocation = VerifyUponInvocation.Yes;
            var mock1 = new Mock<IToMock>();
            var mock2 = new Mock<IOtherToMock>();

            mock1.SetupInSequenceCreateShared(m => m.Method1(), out var sequence, 1, 2);
            mock2.SetupInSequenceShared(m => m.Method1(), sequence, 2);

            mock1.Object.Method1();
            mock2.Object.Method1();
            mock2.Object.Method1();

            mock1.Object.Method1();
            mock2.Object.Method1();
            mock2.Object.Method1();

            SequenceExceptionAssertions.Throws_Additional_Calls(()=> mock2.Object.Method1(), 6, 7);
        }
        [Test]
        public void Sequence_Value_Defaults_To_Sequences_Value_If_Not_Set()
        {
            Sequences.VerifyUponInvocation = VerifyUponInvocation.Yes;
            var mock = new Mock<IToMock>();
            mock.SetupInSequenceCreateShared(m => m.Method1(), out var sequence1);
            mock.SetupInSequenceShared(m => m.MethodWithArgument("1"), sequence1);

            mock.SetupInSequenceCreateShared(m => m.Method2(), out var sequence2);
            mock.SetupInSequenceShared(m => m.MethodWithArgument("2"), sequence2);
            sequence2.VerifyUponInvocation = VerifyUponInvocation.No;

            var mocked = mock.Object;
            Assert.That(() => mocked.MethodWithArgument("2"), Throws.Nothing);
            SequenceExceptionAssertions.Throws_Out_Of_Sequence(() => mocked.MethodWithArgument("1"), 0);
        }
        [TestCase(VerifyUponInvocation.No,MockBehavior.Loose,MockBehavior.Loose,ExpectedResult =false)]
        [TestCase(VerifyUponInvocation.Yes, MockBehavior.Loose, MockBehavior.Loose, ExpectedResult = true)]
        [TestCase(VerifyUponInvocation.Strict, MockBehavior.Loose, MockBehavior.Loose, ExpectedResult = false)]
        [TestCase(VerifyUponInvocation.Strict, MockBehavior.Loose, MockBehavior.Strict, ExpectedResult = false)]
        [TestCase(VerifyUponInvocation.Strict, MockBehavior.Strict, MockBehavior.Strict, ExpectedResult = true)]
        public bool Verify_Upon_Call_Execution_Depends_On_Verify_Upon_Call_And_MockBehavior(VerifyUponInvocation verifyUponCall,MockBehavior mock1Behavior,MockBehavior mock2Behavior)
        {
            var mock = new Mock<IToMock>(mock1Behavior);
            mock.SetupInSequenceCreateShared(m => m.Method1(), out var sequence);
            var mock2 = new Mock<IOtherToMock>(mock2Behavior);
            mock2.SetupGetInSequenceShared(m => m.SomeProp, sequence).Returns(1);//to satisfy strict
            sequence.VerifyUponInvocation = verifyUponCall;

            //this would throw if verifying upon call
            return ThrewHelper.Threw<SequenceVerifyException>(() =>
            {
                var _ = mock2.Object.SomeProp;
            },e=>e.OutOfSequence);
        }
    }

    [TestFixture]
    [Ignore("refactoring tests")]
    public class Sequences_Tests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Sequence.Factory = new SequenceFactory();
            Invocable.Factory = new InvocableFactory();
        }
        [TestCase(true, true, ExpectedResult = false)]
        [TestCase(true, false, ExpectedResult = true)]
        [TestCase(false, true, ExpectedResult = true)]
        [TestCase(false, false, ExpectedResult = true)]
        public bool Can_Verify_Multiple(bool completeSequence1, bool completeSequence2)
        {
            var mock = new Mock<IToMock>();
            mock.SetupInSequenceCreateShared(m => m.Method1(), out var sequence1);
            mock.SetupInSequenceCreateShared(m => m.Method2(), out var sequence2);
            if (completeSequence1)
            {
                mock.Object.Method1();
            }
            if (completeSequence2)
            {
                mock.Object.Method2();
            }
            return ThrewHelper.Threw(() => Sequences.Verify(sequence1, sequence2));
        }
    }
    [TestFixture]
    [Ignore("refactoring tests")]
    public class InSequence_Sequences_Returns_Test
    {
        [Test]
        public void Should_Throw_When_Number_Of_Returns_Differs_To_Number_Of_Sequence_Invocation_Indices()
        {
            var mock = new Mock<IToMock>();
            Assert.That(()=> mock.SetupInSequenceSingle(m => m.MethodWithArgument(It.IsAny<string>()), new Returns<string> { "1", "2", "3" },new SequenceInvocationIndices { 0,1}),Throws.ArgumentException);
        }


        [TestCase(true)]
        [TestCase(false)]
        public void Should_Work_Like_Times(bool completeSequence)
        {
            var mock = new Mock<IToMock>();
            mock.SetupInSequenceSingle(m => m.Method1());
            mock.SetupInSequenceSingle(m => m.MethodWithArgument(It.IsAny<string>()), new Returns<string> { "1", "2", "3" });
            mock.SetupInSequenceSingle(m => m.Method2());
            var mocked = mock.Object;

            mocked.Method1();
            var sequenceReturn1 = mocked.MethodWithArgument("");
            var sequenceReturn2 = mocked.MethodWithArgument("");
            var sequenceReturn3 = mocked.MethodWithArgument("");

            Assert.That(sequenceReturn1, Is.EqualTo("1"));
            Assert.That(sequenceReturn2, Is.EqualTo("2"));
            Assert.That(sequenceReturn3, Is.EqualTo("3"));

            if (completeSequence)
            {
                mocked.Method2();
                Assert.That(() => mock.VerifySequence(), Throws.Nothing);
            }
            else
            {
                SequenceExceptionAssertions.Throws_Insufficient_Calls(mock, 4, 1);
            }
        }
        [TestCase(true)]
        [TestCase(false)]
        public void Should_Work_With_Loops(bool invokeLast)
        {
            var mock = new Mock<IToMock>();
            mock.SetupInSequenceSingle(m => m.MethodWithArgument(It.IsAny<string>()), new Returns<string> { "1", "2", "3" },2);
            var mocked = mock.Object;

            var sequenceReturn1 = mocked.MethodWithArgument("");
            var sequenceReturn2 = mocked.MethodWithArgument("");
            var sequenceReturn3 = mocked.MethodWithArgument("");

            var sequenceReturn4 = mocked.MethodWithArgument("");
            var sequenceReturn5 = mocked.MethodWithArgument("");
            

            Assert.That(sequenceReturn1, Is.EqualTo("1"));
            Assert.That(sequenceReturn2, Is.EqualTo("2"));
            Assert.That(sequenceReturn3, Is.EqualTo("3"));
            Assert.That(sequenceReturn4, Is.EqualTo("1"));
            Assert.That(sequenceReturn5, Is.EqualTo("2"));
            if (invokeLast)
            {
                var sequenceReturn6 = mocked.MethodWithArgument("");
                Assert.That(sequenceReturn6, Is.EqualTo("3"));
                Assert.That(() => mock.VerifySequence(), Throws.Nothing);
            }
            else
            {
                Assert.That(() => mock.VerifySequence(), Throws.InstanceOf<SequenceVerifyException>());
            }

            
        }
        
        [Test]
        public void Should_Work_Like_CallIndices()
        {
            Sequences.VerifyUponInvocation = VerifyUponInvocation.Strict;
            var mock = new Mock<IToMock>(MockBehavior.Strict);
            mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), new Returns<string> {"1", "2" },new SequenceInvocationIndices { 0,2});
            mock.SetupGetInSequenceSingle(m => m.Prop, SequenceInvocationIndices.Singular(1)).Returns(0);

            var mocked = mock.Object;
            var sequenceResult1 = mocked.MethodWithArgument("");
            var _ = mocked.Prop;
            var sequenceResult2 = mocked.MethodWithArgument("");
            Assert.That(sequenceResult1, Is.EqualTo("1"));
            Assert.That(sequenceResult2, Is.EqualTo("2"));
            
        }
        
        [Test]
        public void Exceptions_Should_Work()
        {
            var mock = new Mock<IToMock>();
            var exception = new ArgumentException();
            var result1 = "";
            var result2 = "";
            mock.SetupInSequenceSingle(m => m.MethodWithArgument(""), 
                new ExceptionsOrReturns<string> {
                    ExceptionOrReturnFactory.Return(result1),
                    ExceptionOrReturnFactory.Exception<string>(exception),
                    ExceptionOrReturnFactory.Return(result2)});

            var mocked = mock.Object;
            var sequenceResult1 = mocked.MethodWithArgument("");
            Assert.That(sequenceResult1, Is.EqualTo(result1));
            Assert.That(() => mocked.MethodWithArgument(""), Throws.Exception.SameAs(exception));
            var sequenceResult2 = mocked.MethodWithArgument("");
            Assert.That(sequenceResult2, Is.EqualTo(result2));
        }
        [Test]//will need to do one for reference types and the other for value types
        public void Should_Behave_Like_SetupSequence_When_Exhausted()
        {
            var mockSequence = new Mock<IToMock>();
            var mockInSequenceSequence = new Mock<IToMock>();

            mockSequence.SetupSequence(m => m.MethodWithArgument("")).Returns("1").Returns("2");
            mockInSequenceSequence.SetupInSequenceSingle(m => m.MethodWithArgument(""), new Returns<string> { "1", "2" });

            var mockedSequence = mockSequence.Object;
            var mockedInSequenceSequence = mockInSequenceSequence.Object;

            mockedSequence.MethodWithArgument("");
            mockedSequence.MethodWithArgument("");
            mockedInSequenceSequence.MethodWithArgument("");
            mockedInSequenceSequence.MethodWithArgument("");

            var exhaustedSequenceValue= mockedSequence.MethodWithArgument("");
            var exhaustedInSequenceSequenceValue = mockedInSequenceSequence.MethodWithArgument("");

            Assert.That(exhaustedSequenceValue, Is.Null);
            Assert.That(exhaustedInSequenceSequenceValue, Is.Null);
        }
        [Test]
        public void Should_Behave_Like_SetupSequence_When_Exhausted_Exceptions()
        {
            var mockSequence = new Mock<IToMock>();
            var mockInSequenceSequence = new Mock<IToMock>();

            mockSequence.SetupSequence(m => m.MethodWithArgument("")).Returns("1").Throws(new Exception());
            mockInSequenceSequence.SetupInSequenceSingle(m => m.MethodWithArgument(""), new ExceptionsOrReturns<string> {
                ExceptionOrReturnFactory.Return("1"),ExceptionOrReturnFactory.Exception<string>(new Exception())
            });

            var mockedSequence = mockSequence.Object;
            var mockedInSequenceSequence = mockInSequenceSequence.Object;

            
            mockedSequence.MethodWithArgument("");
            try
            {
                mockedSequence.MethodWithArgument("");
            }
            catch{ }
            mockedInSequenceSequence.MethodWithArgument("");
            try
            {
                mockedInSequenceSequence.MethodWithArgument("");
            }
            catch { }
            
            var exhaustedSequenceValue = mockedSequence.MethodWithArgument("");
            var exhaustedInSequenceSequenceValue = mockedInSequenceSequence.MethodWithArgument("");

            Assert.That(exhaustedSequenceValue, Is.Null);
            Assert.That(exhaustedInSequenceSequenceValue, Is.Null);
        }
        [Test]//Todo exhausted value type
        public void TestToDo_Value_Type_Default_Values()
        {
            Assert.Fail();
        }

        //Normal verification
        [TestCase(true)]
        [TestCase(false)]
        public void Passes_VerifyAll_If_Called_Once(bool invoke)
        {
            var mockInSequenceSequence = new Mock<IToMock>();
            mockInSequenceSequence.SetupInSequenceSingle(m => m.MethodWithArgument(""), new ExceptionsOrReturns<string> {
                ExceptionOrReturnFactory.Return("1"),ExceptionOrReturnFactory.Exception<string>(new Exception())
            });
            if (invoke)
            {
                mockInSequenceSequence.Object.MethodWithArgument("");
                Assert.That(() => mockInSequenceSequence.VerifyAll(), Throws.Nothing);
            }
            else
            {
                Assert.That(() => mockInSequenceSequence.VerifyAll(), Throws.InstanceOf<MockException>());
            }
        }
        [TestCase(true)]
        [TestCase(false)]
        public void Is_Verifiable_And_Passes_With_Single_Invocation(bool invoke)
        {
            var mockInSequenceSequence = new Mock<IToMock>();
            var exception = new Exception();
            mockInSequenceSequence.SetupInSequenceSingle(m => m.MethodWithArgument(""), new ExceptionsOrReturns<string> {
                ExceptionOrReturnFactory.Return("1"),ExceptionOrReturnFactory.Exception<string>(exception)
            }).Verifiable();
            if (invoke)
            {
                //demonstrating that usual behaviour is maintained
                Assert.That(()=>mockInSequenceSequence.Object.MethodWithArgument(""),Is.EqualTo("1"));

                Assert.That(() => mockInSequenceSequence.Verify(), Throws.Nothing);
                
                //demonstrating that usual behaviour is maintained
                Assert.That(() => mockInSequenceSequence.Object.MethodWithArgument(""), Throws.Exception.EqualTo(exception));
            }
            else
            {
                Assert.That(() => mockInSequenceSequence.Verify(), Throws.InstanceOf<MockException>());
            }
        }
    }
    [TestFixture]
    [Ignore("refactoring tests")]
    public class InSequence_Sequences_PassOrThrows
    {
        [Test]
        public void WorksAsExpected()
        {

        }
    }
}
